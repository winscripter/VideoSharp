using System.Diagnostics;

namespace VideoSharp;

public static partial class Video
{

    /// <summary>
    /// Launches FFmpeg in the background and waits for it to close asynchronously
    /// </summary>
    /// <param name="arg">
    /// Command-line arguments to FFmpeg
    /// </param>
    public static async Task LaunchAndWaitForFFmpegAsync(string arg)
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFmpegPath();
        process.StartInfo.Arguments = arg;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        await process.WaitForExitAsync();
    }

    /// <summary>
    /// Launches FFprobe in the background and waits for it to close asynchronously
    /// </summary>
    /// <param name="arg">
    /// Command-line arguments to FFprobe
    /// </param>
    public static async Task LaunchAndWaitForFFprobeAsync(string arg)
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFprobePath();
        process.StartInfo.Arguments = arg;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        await process.WaitForExitAsync();
    }
}
