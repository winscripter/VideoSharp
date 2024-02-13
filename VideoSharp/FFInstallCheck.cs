namespace VideoSharp;

/// <summary>
/// Implements methods to validate if both
/// FFmpeg and FFprobe are found or not.
/// </summary>
public static class FFInstallCheck
{
    /// <summary>
    /// Throws <see cref="FileNotFoundException" /> if FFmpeg is not found
    /// </summary>
    /// <exception cref="FileNotFoundException">FFmpeg was not found</exception>
    public static void EnsureFFmpegExists()
    {
        if (!File.Exists(GetFFmpegPath()))
        {
            throw new FileNotFoundException("Cannot find ffmpeg. Please make sure ffmpeg is named ffmpeg.exe if on Windows, and ffmpeg otherwise, and it is in the same directory as the assembly");
        }
    }

    /// <summary>
    /// Throws <see cref="FileNotFoundException" /> if FFprobe is not found
    /// </summary>
    /// <exception cref="FileNotFoundException">FFprobe was not found</exception>
    public static void EnsureFFprobeExists()
    {
        if (!File.Exists(GetFFprobePath()))
        {
            throw new FileNotFoundException("Cannot find ffprobe. Please make sure ffprobe is named ffprobe.exe if on Windows, and ffprobe otherwise, and it is in the same directory as the assembly");
        }
    }

    /// <summary>
    /// Gets the path to ffmpeg - ffmpeg.exe if running on Windows, ffmpeg otherwise.
    /// </summary>
    public static string GetFFmpegPath()
    {
        if (OperatingSystem.IsWindows())
        {
            return "ffmpeg.exe";
        }
        else
        {
            return "ffmpeg";
        }
    }

    /// <summary>
    /// Gets the path to ffprobe - ffprobe.exe if running on Windows, ffprobe otherwise.
    /// </summary>
    public static string GetFFprobePath()
    {
        if (OperatingSystem.IsWindows())
        {
            return "ffprobe.exe";
        }
        else
        {
            return "ffprobe";
        }
    }
}
