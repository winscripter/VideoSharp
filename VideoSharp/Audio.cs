namespace VideoSharp;

/// <summary>
/// Represents a class for managing a video using audios
/// </summary>
public static class Audio
{
    /// <summary>
    /// Adds audio to a video
    /// </summary>
    /// <param name="videoFile">Video file where the audio will be added</param>
    /// <param name="audioFile">Audio file that will be added</param>
    /// <param name="outputVideo">Video file that will be saved as</param>
    /// <param name="start">Where does the audio start?</param>
    /// <param name="end">Where does the audio end?</param>
    public static void Add(string videoFile, string audioFile, string outputVideo, float start, float end)
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        Video.LaunchFFmpeg($"-i \"{videoFile}\" -ss {start} -t {end} -i \"{audioFile}\" -map 0:v -map 1:a -c:v copy -shortest \"{outputVideo}\"");
    }

    /// <summary>
    /// Adds audio to a video and waits for FFmpeg to close
    /// </summary>
    /// <param name="videoFile">Video file where the audio will be added</param>
    /// <param name="audioFile">Audio file that will be added</param>
    /// <param name="outputVideo">Video file that will be saved as</param>
    /// <param name="start">Where does the audio start?</param>
    /// <param name="end">Where does the audio end?</param>
    public static void AddWait(string videoFile, string audioFile, string outputVideo, float start, float end)
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        Video.LaunchAndWaitForFFmpeg($"-i \"{videoFile}\" -ss {start} -t {end} -i \"{audioFile}\" -map 0:v -map 1:a -c:v copy -shortest \"{outputVideo}\"");
    }

    /// <summary>
    /// Adds audio to a video and waits for FFmpeg to close asynchronously
    /// </summary>
    /// <param name="videoFile">Video file where the audio will be added</param>
    /// <param name="audioFile">Audio file that will be added</param>
    /// <param name="outputVideo">Video file that will be saved as</param>
    /// <param name="start">Where does the audio start?</param>
    /// <param name="end">Where does the audio end?</param>
    public static async Task AddWaitAsync(string videoFile, string audioFile, string outputVideo, float start, float end)
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        await Video.LaunchAndWaitForFFmpegAsync($"-i \"{videoFile}\" -ss {start} -t {end} -i \"{audioFile}\" -map 0:v -map 1:a -c:v copy -shortest \"{outputVideo}\"");
    }

    /// <summary>
    /// Extracts audio from the video
    /// </summary>
    /// <param name="videoFile">Input video file where audio will be extracted</param>
    /// <param name="audioPath">Audio to extract to</param>
    /// <param name="codec">Codec (mp3LAME by default)</param>
    public static void Extract(string videoFile, string audioPath, string codec = "libmp3lame")
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        Video.LaunchFFmpeg($"-i \"{videoFile}\" -vn -c:a {codec} \"{audioPath}\"");
    }

    /// <summary>
    /// Extracts audio from the video and waits for FFmpeg to close
    /// </summary>
    /// <param name="videoFile">Input video file where audio will be extracted</param>
    /// <param name="audioPath">Audio to extract to</param>
    /// <param name="codec">Codec (mp3LAME by default)</param>
    public static void ExtractWait(string videoFile, string audioPath, string codec = "libmp3lame")
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        Video.LaunchAndWaitForFFmpeg($"-i \"{videoFile}\" -vn -c:a {codec} \"{audioPath}\"");
    }

    /// <summary>
    /// Extracts audio from the video and waits for FFmpeg to close asynchronously
    /// </summary>
    /// <param name="videoFile">Input video file where audio will be extracted</param>
    /// <param name="audioPath">Audio to extract to</param>
    /// <param name="codec">Codec (mp3LAME by default)</param>
    public static async Task ExtractWaitAsync(string videoFile, string audioPath, string codec = "libmp3lame")
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        await Video.LaunchAndWaitForFFmpegAsync($"-i \"{videoFile}\" -vn -c:a {codec} \"{audioPath}\"");
    }

    /// <summary>
    /// Changes volume at a specific range from a video
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="volume">
    /// By how much the volume should be adjusted
    /// (1.0F is the default, e.g. 100%; for 200% use 2.0F; to mute, use 0F)
    /// </param>
    /// <param name="start">Start duration where the audio will be adjusted</param>
    /// <param name="end">End duration where the audio will be kept original afterwards</param>
    /// <param name="outVideoFile">Output video file</param>
    /// <remarks>
    /// It is strongly recommended not to increase the
    /// volume too much.
    /// </remarks>
    public static void ChangeVolume(string videoFile, float volume, float start, float end, string outVideoFile)
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        Video.LaunchFFmpeg($"-i \"{videoFile}\" -af \"volume={volume}:enable='between(t,{start},{end})'\" \"{outVideoFile}\"");
    }

    /// <summary>
    /// Changes volume at a specific range from a video and waits for ffmpeg to exit
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="volume">
    /// By how much the volume should be adjusted
    /// (1.0F is the default, e.g. 100%; for 200% use 2.0F; to mute, use 0F)
    /// </param>
    /// <param name="start">Start duration where the audio will be adjusted</param>
    /// <param name="end">End duration where the audio will be kept original afterwards</param>
    /// <param name="outVideoFile">Output video file</param>
    /// <remarks>
    /// It is strongly recommended not to increase the
    /// volume too much.
    /// </remarks>
    public static void ChangeVolumeWait(string videoFile, float volume, float start, float end, string outVideoFile)
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        Video.LaunchAndWaitForFFmpeg($"-i \"{videoFile}\" -af \"volume={volume}:enable='between(t,{start},{end})'\" \"{outVideoFile}\"");
    }

    /// <summary>
    /// Changes volume at a specific range from a video and waits for ffmpeg to exit asynchronously
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="volume">
    /// By how much the volume should be adjusted
    /// (1.0F is the default, e.g. 100%; for 200% use 2.0F; to mute, use 0F)
    /// </param>
    /// <param name="start">Start duration where the audio will be adjusted</param>
    /// <param name="end">End duration where the audio will be kept original afterwards</param>
    /// <param name="outVideoFile">Output video file</param>
    /// <remarks>
    /// It is strongly recommended not to increase the
    /// volume too much.
    /// </remarks>
    public static async Task ChangeVolumeWaitAsync(string videoFile, float volume, float start, float end, string outVideoFile)
    {
        Video.EnsureFFmpegExists();
        Video.EnsureVideoExists(videoFile, nameof(videoFile));

        await Video.LaunchAndWaitForFFmpegAsync($"-i \"{videoFile}\" -af \"volume={volume}:enable='between(t,{start},{end})'\" \"{outVideoFile}\"");
    }

    /// <summary>
    /// Changes volume at a specific range from a video
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="volume">By how many percents should the volume be adjusted (100 is the default)</param>
    /// <param name="start">Start duration where the audio will be adjusted</param>
    /// <param name="end">End duration where the audio will be kept original afterwards</param>
    /// <param name="outVideoFile">Output video file</param>
    /// <remarks>
    /// It is strongly recommended not to increase the
    /// volume too much.
    /// </remarks>
    public static void ChangeVolume(string videoFile, int volume, float start, float end, string outVideoFile)
    {
        ChangeVolume(videoFile, (float)(volume / 100), start, end, outVideoFile);
    }

    /// <summary>
    /// Changes volume at a specific range from a video and waits for ffmpeg to exit
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="volume">By how many percents should the volume be adjusted (100 is the default)</param>
    /// <param name="start">Start duration where the audio will be adjusted</param>
    /// <param name="end">End duration where the audio will be kept original afterwards</param>
    /// <param name="outVideoFile">Output video file</param>
    /// <remarks>
    /// It is strongly recommended not to increase the
    /// volume too much.
    /// </remarks>
    public static void ChangeVolumeWait(string videoFile, int volume, float start, float end, string outVideoFile)
    {
        ChangeVolumeWait(videoFile, (float)(volume / 100), start, end, outVideoFile);
    }

    /// <summary>
    /// Changes volume at a specific range from a video and waits for ffmpeg to exit asynchronously
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="volume">By how many percents should the volume be adjusted (100 is the default)</param>
    /// <param name="start">Start duration where the audio will be adjusted</param>
    /// <param name="end">End duration where the audio will be kept original afterwards</param>
    /// <param name="outVideoFile">Output video file</param>
    /// <remarks>
    /// It is strongly recommended not to increase the
    /// volume too much.
    /// </remarks>
    public static async Task ChangeVolumeWaitAsync(string videoFile, int volume, float start, float end, string outVideoFile)
    {
        await ChangeVolumeWaitAsync(videoFile, (float)(volume / 100), start, end, outVideoFile);
    }

    /// <summary>
    /// Mutes the audio at the specified start and end duration
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="start">Start duration</param>
    /// <param name="end">End duration</param>
    /// <param name="outVideoFile">Output video file</param>
    public static void Mute(string videoFile, float start, float end, string outVideoFile)
    {
        ChangeVolume(videoFile, 0F, start, end, outVideoFile);
    }

    /// <summary>
    /// Mutes the audio at the specified start and end duration and waits for ffmpeg to exit
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="start">Start duration</param>
    /// <param name="end">End duration</param>
    /// <param name="outVideoFile">Output video file</param>
    public static void MuteWait(string videoFile, float start, float end, string outVideoFile)
    {
        ChangeVolumeWait(videoFile, 0F, start, end, outVideoFile);
    }

    /// <summary>
    /// Mutes the audio at the specified start and end duration and waits for ffmpeg to exit asynchronously
    /// </summary>
    /// <param name="videoFile">Input video file</param>
    /// <param name="start">Start duration</param>
    /// <param name="end">End duration</param>
    /// <param name="outVideoFile">Output video file</param>
    public static async Task MuteWaitAsync(string videoFile, float start, float end, string outVideoFile)
    {
        await ChangeVolumeWaitAsync(videoFile, 0F, start, end, outVideoFile);
    }
}
