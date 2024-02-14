namespace VideoSharp;

public static partial class Video
{
    /// <summary>
    /// Gets the video dimensions (x, y)
    /// </summary>
    /// <param name="video">Path to the video file</param>
    /// <returns>x, y size of the video, respectively</returns>
    /// <exception cref="InvalidOperationException">Video file is invalid</exception>
    public static (int Width, int Height) GetVideoDimensions(string video)
    {
        EnsureFFprobeExists();

        var output = LaunchAndRedirectFFprobeOutput($"-v error -show_entries stream=width,height -of default=noprint_wrappers=1 \"{video}\"");

        try
        {
            return (
                int.Parse(output.Split("\n")[0].Split("=")[1]), // width=1920
                int.Parse(output.Split("\n")[1].Split("=")[1])  // height=1080
            );
        }
        catch
        {
            throw new InvalidOperationException("The video file is invalid");
        }
    }

    /// <summary>
    /// Gets the amount of frames in a video
    /// </summary>
    /// <param name="video">Input video file</param>
    /// <returns>Amount of frames in a video</returns>
    /// <exception cref="InvalidOperationException">The video file is invalid</exception>
    public static int GetFrameCount(string video)
    {
        EnsureFFmpegExists();

        string parameters = $"-v error -select_streams v:0 -count_packets -show_entries stream=nb_read_packets -of csv=p=0 {video}";

        var output = LaunchAndRedirectFFprobeOutput(parameters);

        try
        {
            return int.Parse(output);
        }
        catch
        {
            throw new InvalidOperationException("The video file is invalid");
        }
    }
}
