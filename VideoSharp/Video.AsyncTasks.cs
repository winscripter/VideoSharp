namespace VideoSharp;

public static partial class Video
{
    /// <summary>
    /// Combines frames into a video (with no audio, though).
    /// The difference between <see cref="BuildVideoWithFrames(string, string, int)" />
    /// and this method is that this method will also wait for ffmpeg.exe to close when it's
    /// done. This method does that asynchronously. Use <see cref="BuildVideoWithFramesWait(string, string, int)" />
    /// for a non-asynchronous version of this method.
    /// </summary>
    /// <param name="output">
    /// Output video files
    /// </param>
    /// <param name="frameFormat">
    /// Input frame format. This is handled with the FFmpeg algorithm.
    /// For instance, to specify frames with 4 digits in an ascending
    /// order: frame0001.png, frame0002.png, frame0003.png, ..., with
    /// a png extension, use <c>frame%04d.png</c>
    /// </param>
    /// <param name="fps">Frames per second. Defaults to <b>30</b></param>
    public static async Task BuildVideoWithFramesWaitAsync(string output, string frameFormat, int fps = 30)
    {
        EnsureFFmpegExists();
        EnsureVideoExists(output, nameof(output));

        await LaunchAndWaitForFFmpegAsync($"-framerate {fps} -i {frameFormat} -c:v libx264 -pix_fmt yuv420p {output}");
    }

    /// <summary>
    /// Extracts the <i>n</i>th frame from a video file
    /// </summary>
    /// <param name="video">Video file</param>
    /// <param name="path">Path to the extracted frame</param>
    /// <param name="frameIndex"><i>n</i></param>
    /// <remarks>
    /// The frame index, e.g. <i>n</i>, starts with a 0. For instance,
    /// if you intend to extract the 36th frame from a video, you may've made
    /// a common mistake to enter 36 and get the 37th frame instead - you should
    /// specify 35 to get the 36th frame, and so on.
    /// </remarks>
    public static async Task ExtractNthFrameWaitAsync(string video, string path, int frameIndex)
    {
        EnsureFFmpegExists();
        EnsureVideoExists(video, nameof(video));

        await LaunchAndWaitForFFmpegAsync($"-i \"{video}\" -vf \"select=eq(n,{frameIndex})\" -vframes 1 \"{path}\"");
    }


    /// <summary>
    /// Extracts all frames from a video
    /// every few seconds and waits for ffmpeg to close asynchronously
    /// </summary>
    /// <param name="video">Input video file</param>
    /// <param name="frameFormat">FFmpeg frame format</param>
    /// <param name="seconds">Amount of seconds</param>
    public static async Task ExtractAllFramesEverySecondsWaitAsync(string video, string frameFormat, int seconds)
    {
        EnsureFFmpegExists();
        EnsureVideoExists(video, nameof(video));

        await LaunchAndWaitForFFmpegAsync($"-i \"{video}\" -vf \"select='mod (t,{seconds})'\" -vsync 0 \"{frameFormat}\"");
    }


    /// <summary>
    /// Extracts all frames from a video and waits for ffmpeg to close, asynchronously
    /// </summary>
    /// <param name="video">Input video file</param>
    /// <param name="frameFormat">FFmpeg frame format</param>
    public static async Task ExtractAllFramesWaitAsync(string video, string frameFormat)
    {
        EnsureFFmpegExists();
        EnsureVideoExists(video, nameof(video));

        await LaunchAndWaitForFFmpegAsync($"-i \"{video}\" \"{frameFormat}\"");
    }

    /// <summary>
    /// Extracts all frames from a video
    /// every few frames and waits for ffmpeg to close, asynchronously
    /// </summary>
    /// <param name="video">Input video file</param>
    /// <param name="frameFormat">FFmpeg frame format</param>
    /// <param name="frames">Frame interval</param>
    public static async Task ExtractAllFramesEveryFramesWaitAsync(string video, string frameFormat, int frames)
    {
        EnsureFFmpegExists();
        EnsureVideoExists(video, nameof(video));

        await LaunchAndWaitForFFmpegAsync($"-i \"{video}\" -vf \"select='mod (n,{frames})'\" -vsync 0 \"{frameFormat}\"");
    }

    /// <summary>
    /// Inserts a frame at the specified position and waits for ffmpeg to close, asynchronously
    /// </summary>
    /// <param name="video">Input video file</param>
    /// <param name="frame">Frame that will be inserted</param>
    /// <param name="start">Start duration where the frame will show up</param>
    /// <param name="end">End duration where the frame will disappear and the video will continue playing</param>
    /// <param name="output">Output video file</param>
    public static async Task InsertFrameAtWaitAsync(string video, string frame, float start, float end, string output)
    {
        EnsureFFmpegExists();
        EnsureVideoExists(video, nameof(video));

        await LaunchAndWaitForFFmpegAsync($"-i \"{video}\" -i \"{frame}\" -filter_complex \"[0:v][1:v] overlay=enable='between(t,{start},{end})'[out]\" -map \"[out]\" -map 0:a -c:a copy \"{output}\"");
    }

    /// <summary>
    /// Inserts video to a video and waits for ffmpeg to close, asynchronously
    /// </summary>
    /// <param name="video">Input video</param>
    /// <param name="vid2insert">Video that will be inserted</param>
    /// <param name="start">Start duration when the inserted video starts playing</param>
    /// <param name="end">End duration when the inserted video is preceded by a continuation of the previous video</param>
    /// <param name="output">Output video file</param>
    public static async Task InsertVideoWaitAsync(string video, string vid2insert, float start, float end, string output)
    {
        EnsureFFmpegExists();
        EnsureVideoExists(video, nameof(video));

        await LaunchAndWaitForFFmpegAsync($"-i \"{video}\" -i \"{vid2insert}\" -filter_complex \"[0:v][1:v] overlay=enable='between(t,{start},{end})'[out]\" -map \"[out]\" -map 0:a -c:a copy \"{output}\"");
    }
}
