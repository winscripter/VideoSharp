using ConsoleHost;
using System.Globalization;
using VideoSharp;

DoAndExitIfCount(0, () =>
{
    Help();
});

if (args.Length >= 1)
{
    string vf = args[0];

    if (!string.IsNullOrWhiteSpace(vf))
    {
        if (!File.Exists(vf))
        {
            DoAndExit(() =>
            {
                Console.WriteLine("ERROR: Video file was not found");
            });
        }
    }

    DoIfCount(1, () =>
    {
        Console.WriteLine($"Fetching video information...{Environment.NewLine}");

        try
        {
            Console.WriteLine(VidInfo.HumanReadable(vf));
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(ex.StackTrace);
            Console.ResetColor();            
        }
    });

    DoIfMinCount(2, () =>
    {
        switch (args[1].ToLowerInvariant().Trim())
        {
            case "addframe":
                {
                    var src = ArgumentToTimeSpan(2);
                    var dest = ArgumentToTimeSpan(3);

                    var frame = args[4];
                    var output = args[5];

                    Video.InsertFrameAtWait(vf, frame, src, dest, output);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "addvid":
                {
                    var start = ArgumentToTimeSpan(2);
                    var end = ArgumentToTimeSpan(3);

                    var vid = args[4];
                    var output = args[5];

                    Video.InsertVideoWait(vf, vid, start, end, output);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "setaud":
                {
                    var start = ArgumentToTimeSpan(2);
                    var end = ArgumentToTimeSpan(3);

                    var aud = args[4];
                    var output = args[5];

                    Audio.AddWait(vf, aud, output, start, end);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "setaudvol":
                {
                    var start = ArgumentToTimeSpan(2);
                    var end = ArgumentToTimeSpan(3);

                    var output = args[4];

                    var percentage = args[5];

                    Audio.ChangeVolumeWait(vf, (float)(int.Parse(percentage) / 100), start, end, output);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "extractnframe":
                {
                    var n = int.Parse(args[2]);
                    var output = args[3];

                    Video.ExtractNthFrameWait(vf, output, n);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "extractframes":
                {
                    var fmt = args[2];

                    Video.ExtractAllFramesWait(vf, fmt);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "extractframeseveryframes":
                {
                    var fmt = args[2];
                    var interval = int.Parse(args[3]);

                    Video.ExtractAllFramesEveryFramesWait(vf, fmt, interval);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "extractaudio":
                {
                    string output = args[2];

                    try
                    {
                        string codec = args[3];
                        Audio.ExtractWait(vf, output, codec);
                    }
                    catch
                    {
                        Audio.ExtractWait(vf, output);
                    }

                    Console.WriteLine("Operation completed");
                }
                break;
            case "muteat":
                {
                    var start = ArgumentToTimeSpan(2);
                    var end = ArgumentToTimeSpan(3);

                    string output = args[4];

                    Audio.MuteWait(vf, start, end, output);
                    Console.WriteLine("Operation completed");
                }
                break;
            case "getx":
                {
                    Console.WriteLine(Video.GetVideoDimensions(vf).Width);
                }
                break;
            case "gety":
                {
                    Console.WriteLine(Video.GetVideoDimensions(vf).Height);
                }
                break;
            case "getxy":
                {
                    var (Width, Height) = Video.GetVideoDimensions(vf);
                    Console.WriteLine($"{Width}x{Height}");
                }
                break;
            case "chkffmpeg":
                {
                    Console.WriteLine(File.Exists(FFInstallCheck.GetFFmpegPath()));
                }
                break;
            case "chkffprobe":
                {
                    Console.WriteLine(File.Exists(FFInstallCheck.GetFFprobePath()));
                }
                break;
            case "build":
                {
                    string frameFormat = args[2];
                    string output = args[3];
                    int fps = int.Parse(args[4]);

                    Video.BuildVideoWithFramesWait(output, frameFormat, fps);
                    Console.WriteLine("Operation completed");
                }
                break;
            default:
                Console.WriteLine("Unrecognized parameter. Use ConsoleHost without any arguments for help.");
                break;
        }
    });
}

static void SuccessExit()
{
    Environment.Exit(0);
}

static void DoAndExit(Action code)
{
    code();
    SuccessExit();
}

void DoIfCount(int count, Action action)
{
    if (args.Length == count)
    {
        action();
    }
}

void DoIfMinCount(int count, Action action)
{
    if (args.Length >= count)
    {
        action();
    }
}

void DoAndExitIfCount(int count, Action action)
{
    DoIfCount(count, action);

    DoIfCount(count, () =>
    {
        Environment.Exit(0);
    });
}

float ArgumentToTimeSpan(int index)
{
    var time = args[index].ToTimeSpan();

    return float.Parse($"{(int)time.TotalSeconds}.{time.Milliseconds}", CultureInfo.InvariantCulture);
}

static void Help()
{
    const string message = @"ConsoleHost is a VideoSharp tool that acts like a bridge to ffmpeg or ffprobe in a simpler way. It also acts like a demonstration on how to use VideoSharp.

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

Please note that this program is not intended for general-purpose use, but rather for developers to quickly learn how to use VideoSharp.";
    Console.WriteLine(message);
}
