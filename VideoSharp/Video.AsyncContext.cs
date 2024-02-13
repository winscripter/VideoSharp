namespace VideoSharp;

public static partial class Video
{
    /// <summary>
    /// Gets the video dimensions (x, y)
    /// </summary>
    /// <param name="video">Path to the video file</param>
    /// <returns>x, y size of the video, respectively</returns>
    /// <exception cref="InvalidOperationException">Video file is invalid</exception>
    public static async Task<(int Width, int Height)> GetVideoDimensionsAsync(string video)
    {
        EnsureFFprobeExists();

        var output = await LaunchAndRedirectFFprobeOutputAsync($"-v error -show_entries stream=width,height -of default=noprint_wrappers=1 \"{video}\"");

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
}
