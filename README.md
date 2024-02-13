# VideoSharp
A video context and basic editing library, bundled with practically all the goods you need. Operates on FFmpeg and FFprobe

# Prerequisites
After building VideoSharp on .NET 6, you need to download FFmpeg. After that, extract the archive, and drag & drop **ffmpeg** and **ffprobe** into the build directory, e.g. same nest level as **VideoSharp.dll**.

Links to get FFmpeg:

[For any operating system](https://www.ffmpeg.org/download.html)
[For Windows](https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-full.7z)

And yes, I'm aware extracting both ffmpeg and ffprobe on Windows results in 258MB worth of 2 files. *Video editing is an intensive computer interaction!*

# Compatibility
VideoSharp only runs on Windows, macOS, and Linux. It mainly depends if you even download a compatible version of FFmpeg and FFprobe.
VideoSharp will not run on the web, because multitasking on the web is not **yet** a thing.
