using OpenCvSharp;
using System.Net.Http;

const string cascadeFileName = "haarcascade_frontalface_default.xml";
const string cascadeUrl =
    "https://raw.githubusercontent.com/opencv/opencv/master/data/haarcascades/haarcascade_frontalface_default.xml";

var appDirectory = AppContext.BaseDirectory;
var cascadePath = Path.Combine(appDirectory, cascadeFileName);

if (!File.Exists(cascadePath))
{
    Console.WriteLine("Haar cascade file not found. Downloading...");
    using var httpClient = new HttpClient();
    var cascadeBytes = await httpClient.GetByteArrayAsync(cascadeUrl);
    await File.WriteAllBytesAsync(cascadePath, cascadeBytes);
    Console.WriteLine("Haar cascade downloaded.");
}

using var faceCascade = new CascadeClassifier(cascadePath);
if (faceCascade.Empty())
{
    Console.WriteLine("Failed to load cascade file.");
    return;
}

// AVFoundation is usually the most stable backend on macOS.
using var capture = new VideoCapture(0, VideoCaptureAPIs.AVFOUNDATION);
if (!capture.IsOpened())
{
    Console.WriteLine("Camera could not be opened. Make sure camera permission is granted.");
    return;
}

Console.WriteLine("Face detection started. Press 'q' while the window is focused to quit.");

using var window = new Window("Face Detection - macOS (C#)");
using var frame = new Mat();
using var gray = new Mat();

while (true)
{
    capture.Read(frame);
    if (frame.Empty())
    {
        continue;
    }

    // Flip horizontally for a mirrored camera view.
    Cv2.Flip(frame, frame, FlipMode.Y);

    Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
    Cv2.EqualizeHist(gray, gray);

    var faces = faceCascade.DetectMultiScale(
        gray,
        scaleFactor: 1.1,
        minNeighbors: 5,
        flags: HaarDetectionTypes.ScaleImage,
        minSize: new Size(60, 60)
    );

    foreach (var face in faces)
    {
        Cv2.Rectangle(frame, face, Scalar.LimeGreen, 2);
    }

    window.ShowImage(frame);

    var key = Cv2.WaitKey(1);
    if (key == 'q' || key == 'Q')
    {
        break;
    }
}
