using System.Diagnostics;

namespace VideoSharp;

public static partial class Video
{
    /// <summary>
    /// Launches FFmpeg in the background
    /// </summary>
    /// <param name="arg">
    /// Command-line arguments to FFmpeg
    /// </param>
    public static void LaunchFFmpeg(string arg)
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFmpegPath();
        process.StartInfo.Arguments = arg;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
    }

    /// <summary>
    /// Launches FFmpeg in the background and waits for it to close
    /// </summary>
    /// <param name="arg">
    /// Command-line arguments to FFmpeg
    /// </param>
    public static void LaunchAndWaitForFFmpeg(string arg)
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFmpegPath();
        process.StartInfo.Arguments = arg;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();
    }


    /// <summary>
    /// Gets the process instance template for FFmpeg
    /// </summary>
    public static Process GetFFmpegProcessTemplate()
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFmpegPath();
        process.StartInfo.Arguments = string.Empty;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        return process;
    }
}
