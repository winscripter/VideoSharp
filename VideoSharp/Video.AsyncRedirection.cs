namespace VideoSharp;

public static partial class Video
{
    /// <summary>
    /// Launches FFmpeg in the background with specified arguments,
    /// and once the process is done, returns its standard output.
    /// </summary>
    /// <param name="arg">Command-line arguments</param>
    /// <returns>Standard output</returns>
    public static async Task<string> LaunchAndRedirectFFmpegOutputAsync(string arg)
    {
        var template = GetFFmpegProcessTemplate();

        template.StartInfo.Arguments = arg;
        template.StartInfo.RedirectStandardOutput = true;

        template.Start();
        string s = await template.StandardOutput.ReadToEndAsync();
        await template.WaitForExitAsync();

        return s;
    }

    /// <summary>
    /// Launches FFprobe in the background with specified arguments,
    /// and once the process is done, returns its standard output.
    /// </summary>
    /// <param name="arg">Command-line arguments</param>
    /// <returns>Standard output</returns>
    public static async Task<string> LaunchAndRedirectFFprobeOutputAsync(string arg)
    {
        var template = GetFFprobeProcessTemplate();

        template.StartInfo.Arguments = arg;
        template.StartInfo.RedirectStandardOutput = true;

        template.Start();
        string s = await template.StandardOutput.ReadToEndAsync();
        await template.WaitForExitAsync();

        return s;
    }
}
