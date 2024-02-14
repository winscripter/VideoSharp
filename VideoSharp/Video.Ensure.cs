namespace VideoSharp;

public static partial class Video
{
    /// <summary>
    /// Makes sure video file exists. Throws <see cref="FileNotFoundException" />
    /// if it doesn't. In fact, this applies to any file.
    /// </summary>
    /// <param name="path">Input file (primarily video file)</param>
    /// <param name="pName">Parameter name that's passed to an exception</param>
    /// <exception cref="FileNotFoundException">Video file is not found</exception>
    public static void EnsureVideoExists(string path, string? pName = null)
    {
        pName ??= nameof(pName);

        if (!File.Exists(path))
        {
            throw new ArgumentException($"Video file cannot be found", pName);
        }
    }

    /// <summary>
    /// Alias for <see cref="FFInstallCheck.EnsureFFmpegExists" />
    /// </summary>
    public static void EnsureFFmpegExists()
    {
        FFInstallCheck.EnsureFFmpegExists();
    }

    /// <summary>
    /// Alias for <see cref="FFInstallCheck.EnsureFFprobeExists" />
    /// </summary>
    public static void EnsureFFprobeExists()
    {
        FFInstallCheck.EnsureFFprobeExists();
    }
}
