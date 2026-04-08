# Face Detection on macOS with C#

This project is a real-time face detection app built with C# and OpenCvSharp.

## Project Structure

- `FaceDetectionMac/`: .NET console app source
- `FaceDetectionMac/Program.cs`: webcam capture + face detection logic

## Requirements

- macOS (Apple Silicon tested)
- .NET SDK 10+
- Camera permission for your terminal app (Terminal/iTerm/etc.)

## NuGet Packages

The app uses these packages:

- `OpenCvSharp4` (`4.7.0.20230115`)
- `OpenCvSharp4.runtime.osx.10.15-universal` (`4.7.0.20230224`)

## Run

```bash
cd ~/Desktop/faceDetection
dotnet run --project FaceDetectionMac/FaceDetectionMac.csproj
```

## Controls

- Press `q` to quit.

## Features

- Real-time webcam capture
- Face detection using Haar Cascade
- Mirrored camera view (horizontal flip)
- Automatic download of `haarcascade_frontalface_default.xml` if missing

## Troubleshooting

### Camera permission error

If you see messages like:

- `not authorized to capture video`
- `Camera could not be opened`

Enable camera access:

1. Open `System Settings` -> `Privacy & Security` -> `Camera`
2. Enable your terminal app (for example `Terminal` or `iTerm`)
3. Restart terminal and run again

Optional reset for Terminal:

```bash
tccutil reset Camera com.apple.Terminal
```

