# VideoSharp is no longer being maintained!
VideoSharp is not maintained anymore, despite the fact it's a new library. *It is for a good reason, though*!
Try [dotnetVEE](https://github.com/winscripter/dotnetVEE). It is a new Video Editing library and is maintained. It is a lot more
powerful than VideoSharp and has the same prerequisites.

# VideoSharp
A video context and basic editing library, bundled with practically all the goods you need. Operates on FFmpeg and FFprobe

# Prerequisites
After building VideoSharp on .NET 6, you need to download FFmpeg. After that, extract the archive, and drag & drop **ffmpeg** and **ffprobe** into the build directory, e.g. same nest level as **VideoSharp.dll**.

Links to get FFmpeg:

[For any operating system](https://www.ffmpeg.org/download.html) <br />
[For Windows](https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-full.7z)

And yes, I'm aware extracting both ffmpeg and ffprobe on Windows results in 258MB worth of 2 files. *Video editing is an intensive computer interaction!*

# Compatibility
VideoSharp only runs on Windows, macOS, and Linux. It mainly depends if you even download a compatible version of FFmpeg and FFprobe.
VideoSharp will not run on the web, because multitasking on the web is not **yet** a thing.

# ConsoleHost for VideoSharp
ConsoleHost is a special, easy-to-use tool. It's a command-line program that invokes VideoSharp functions.
You can use it like a video editor. You can also take a look at its source code to learn how to use VideoSharp.

Before I provide a list of parameters, I just have to mention: parameters starting with *time__*, for instance, **time__start**, means it is a representation of time, which is in the following format:
`<hours>:<minutes>:<seconds>.<milliseconds>`
Specifying hours, minutes, seconds, and milliseconds are all mandatory.

To specify 32.5 seconds:
`00:00:32.500`

Here is a list of parameters:

        ConsoleHost video.mp4 addframe <time__start> <time__end> <framePath> <output>
        Inserts a frame to video.mp4. Starts at <time__start> and lasts until <time__end>. The frame is <framePath>. The result video is saved as <output>

        ConsoleHost video.mp4 addvid <time__start> <time__end> <vidPath> <output>
        Inserts another video to video.mp4. Starts at <time__start> and lasts until <time__end>. The video is <vidPath>. The result video is saved as <output>

        ConsoleHost video.mp4 setaud <time__start> <time__end> <audPath> <output>
        Adds audio to video.mp4. Starts at <time__start> and lasts until <time__end>. The audio is <audPath>. The result video is saved as <output>

        ConsoleHost video.mp4 setaudvol <time__start> <time__end> <audVol> <output>
        Sets the audio volume at video.mp4. Effect starts at <time__start> and lasts until <time__end>. The volume is <audVol>. It is interpreted as percents (e.g. if audVol is 100, the effect doesn't change, but if audVol is 169, the audio is louder by 1.69x). The result video is saved as <output>

        ConsoleHost video.mp4 extractnframe <n> <output>
        Extracts the <n>th frame from video.mp4 and saves it as <output>

        ConsoleHost video.mp4 extractframes <format>
        Extracts all frames from video.mp4 and saves them as the FFmpeg <format>. For instance, specifying ""frame%04d.png"" saves frames in an ascending order with 4 digits, e.g. frame0000.png, frame0001.png, frame0002.png, frame0003.png, etc. **CAUTION**: Extracting frames can result in a massive amount of data! To get some perspective, extracting all frames from a 1920x1080 30fps 13 second mp4 results in a staggering 208MB worth of frames, which can be significant if the video is longer and has better quality
        
        ConsoleHost video.mp4 extractframeseveryframes <format> <interval>
        Extracts all frames from video.mp4 every <internal> frames and saves them as the FFmpeg <format>. For instance, specifying ""frame%04d.png"" saves frames in an ascending order with 4 digits, e.g. frame0000.png, frame0001.png, frame0002.png, frame0003.png, etc.

        ConsoleHost video.mp4 extractaudio <output>
        Extracts audio from the video file as <output>
        
        ConsoleHost video.mp4 muteat <time__start> <time__end> <output>
        Mutes audio from within the video within the specified duration, and saves the video as <output>
        
        ConsoleHost video.mp4 getx
        Outputs the width of the video in pixels
        
        ConsoleHost video.mp4 gety
        Outputs the height of the video in pixels
        
        ConsoleHost video.mp4 getxy
        Outputs the video dimension, f.e. 1920x1080
        
        ConsoleHost """" chkffmpeg
        Outputs ""True"" if ffmpeg is found, ""False"" otherwise
        
        ConsoleHost """" chkffprobe
        Outputs ""True"" is ffprobe is found, ""False"" otherwise
        
        ConsoleHost """" build <frameFormat> <output> <fps>
        Builds a video using a series of images. They are detected with the FFmpeg <frameFormat>. F.e. frame%04d.png makes VideoSharp select every video file that starts with frame0000.png, in an ascending other (frame0000.png, frame0001.png, frame0002.png, etc). The Frames Per Second (FPS) is defined with <fps>

Please note that this program is not intended for general-purpose use, but rather for developers to quickly learn how to use VideoSharp.

To begin with ConsoleHost, you should compile it first, for .NET 6. After building using tools like **MSBuild**, **Visual Studio (Recommended)**, **JetBrains Rider**, etc, you can locate ConsoleHost.exe in the bin\Debug\net6.0 folder.
That is exactly ConsoleHost, the one you may need!
