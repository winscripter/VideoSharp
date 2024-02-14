using System.Diagnostics;

namespace VideoSharp;

public static partial class Video
{

    /// <summary>
    /// Gets the process instance template for FFprobe
    /// </summary>
    public static Process GetFFprobeProcessTemplate()
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFprobePath();
        process.StartInfo.Arguments = string.Empty;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        return process;
    }

    /// <summary>
    /// Launches FFprobe in the background
    /// </summary>
    /// <param name="arg">
    /// Command-line arguments to FFprobe
    /// </param>
    public static void LaunchFFprobe(string arg)
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFprobePath();
        process.StartInfo.Arguments = arg;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
    }

    /// <summary>
    /// Launches FFprobe in the background and waits for it to close
    /// </summary>
    /// <param name="arg">
    /// Command-line arguments to FFprobe
    /// </param>
    public static void LaunchAndWaitForFFprobe(string arg)
    {
        var process = new Process();

        process.StartInfo.FileName = FFInstallCheck.GetFFprobePath();
        process.StartInfo.Arguments = arg;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();
    }
}
