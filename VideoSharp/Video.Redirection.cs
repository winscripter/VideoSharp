namespace VideoSharp;

public static partial class Video
{
    /// <summary>
    /// Launches FFprobe in the background with specified arguments,
    /// and once the process is done, returns its standard output.
    /// </summary>
    /// <param name="arg">Command-line arguments</param>
    /// <returns>Standard output</returns>
    public static string LaunchAndRedirectFFprobeOutput(string arg)
    {
        var template = GetFFprobeProcessTemplate();

        template.StartInfo.Arguments = arg;
        template.StartInfo.RedirectStandardOutput = true;

        template.Start();
        string s = template.StandardOutput.ReadToEnd();
        template.WaitForExit();

        return s;
    }


    /// <summary>
    /// Launches FFmpeg in the background with specified arguments,
    /// and once the process is done, returns its standard output.
    /// </summary>
    /// <param name="arg">Command-line arguments</param>
    /// <returns>Standard output</returns>
    public static string LaunchAndRedirectFFmpegOutput(string arg)
    {
        var template = GetFFmpegProcessTemplate();

        template.StartInfo.Arguments = arg;
        template.StartInfo.RedirectStandardOutput = true;

        template.Start();
        string s = template.StandardOutput.ReadToEnd();
        template.WaitForExit();

        return s;
    }
}
