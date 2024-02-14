namespace VideoSharp;

/// <summary>
/// Represents information about a video file
/// </summary>
public class VideoContext
{
    private readonly string _path;
    private Dimension _dimension;
    private float _fps;
    private int _frameCount;
    private bool _initialized;

    /// <summary>
    /// Initializes a new instance of <see cref="VideoContext" /> with the path to the video file.
    /// </summary>
    /// <param name="path">Path to the video file.</param>
    public VideoContext(string path)
    {
        _path = path;
        _initialized = false;
        _dimension = new Dimension(0, 0); // default

        _fps = _frameCount = 0;
    }

    /// <summary>
    /// Asynchronously or synchronously initializes this instance of <see cref="VideoContext" />
    /// </summary>
    /// <returns>Nothing</returns>
    public async Task Initialize(ActionCallType act)
    {
        if (act == ActionCallType.None)
        {
            InitializeNoWait();
        }
        else
        {
            await InitializeAsync();
        }
    }

    /// <summary>
    /// Asynchronously initializes this instance of <see cref="VideoContext" />
    /// </summary>
    /// <returns>Nothing</returns>
    public async Task AsyncInit()
    {
        await Initialize(ActionCallType.WaitAsync);
    }

    /// <summary>
    /// Synchronously initializes this instance of <see cref="VideoContext" />
    /// </summary>
    /// <returns>Nothing</returns>
    public void SyncInit()
    {
        InitializeNoWait();
    }
    
    private void InitializeNoWait()
    {
        if (!_initialized)
        {
            _initialized = !_initialized;

            _fps = Video.GetFpsCount(_path);
            
            var (Width, Height) = Video.GetVideoDimensions(_path);
            _dimension = new(Width, Height);

            _frameCount = Video.GetFrameCount(_path);
        }
    }

    private async Task InitializeAsync()
    {
        if (!_initialized)
        {
            _initialized |= !_initialized;

            _fps = await Video.GetFpsCountAsync(_path);

            var (Width, Height) = Video.GetVideoDimensions(_path);
            _dimension = new(Width, Height);

            _frameCount = Video.GetFrameCount(_path);
        }
    }

    /// <summary>
    /// Returns the width and height of this video file in pixels
    /// </summary>
    public Dimension Dimension
    {
        get
        {
            return _dimension;
        }
    }

    /// <summary>
    /// Gets the total amount of frames in this video file
    /// </summary>
    public int FrameCount
    {
        get
        {
            return _frameCount;
        }
    }

    /// <summary>
    /// Gets the amount of Frames Per Second (FPS) in this video file
    /// </summary>
    public float Fps
    {
        get
        {
            return _fps;
        }
    }

    /// <summary>
    /// Returns the path to this video file
    /// </summary>
    public string Path
    {
        get
        {
            return _path;
        }
    }

    /// <summary>
    /// Is this instance initialized?
    /// </summary>
    public bool IsInitialized
    {
        get
        {
            return _initialized;
        }
    }

    /// <summary>
    /// Extracts the nth frame.
    /// </summary>
    /// <remarks>
    /// The frame index starts with 0 (f.e. to extract the 36th frame, pass 35 to parameter <c>frame</c> instead)
    /// </remarks>
    public void SaveNthFrame(int frame, string output = "./output.png")
    {
        Video.ExtractNthFrame(Path, output, frame);
    }

    /// <summary>
    /// Extracts the nth frame.
    /// </summary>
    /// <remarks>
    /// The frame index starts with 0 (f.e. to extract the 36th frame, pass 35 to parameter <c>frame</c> instead)
    /// </remarks>
    public void SaveNthFrameWait(int frame, string output = "./output.png")
    {
        Video.ExtractNthFrameWait(Path, output, frame);
    }

    /// <summary>
    /// Extracts the nth frame.
    /// </summary>
    /// <remarks>
    /// The frame index starts with 0 (f.e. to extract the 36th frame, pass 35 to parameter <c>frame</c> instead)
    /// </remarks>
    public async Task SaveNthFrameAsync(int frame, string output = "./output.png")
    {
        await Video.ExtractNthFrameWaitAsync(Path, output, frame);
    }

    /// <summary>
    /// Extracts frames from this video in an interval of custom amount of seconds
    /// </summary>
    /// <param name="frameFormat">FFmpeg frame foramt</param>
    /// <param name="seconds">Amount of seconds</param>
    public void ExtractEverySeconds(string frameFormat, int seconds)
    {
        Video.ExtractAllFramesEverySeconds(Path, frameFormat, seconds);
    }

    /// <summary>
    /// Extracts frames from this video in an interval of custom amount of seconds,
    /// and waits for ffmpeg to close
    /// </summary>
    /// <param name="frameFormat">FFmpeg frame foramt</param>
    /// <param name="seconds">Amount of seconds</param>
    public void ExtractEverySecondsWait(string frameFormat, int seconds)
    {
        Video.ExtractAllFramesEverySecondsWait(Path, frameFormat, seconds);
    }

    /// <summary>
    /// Extracts frames from this video in an interval of custom amount of seconds,
    /// and waits for ffmpeg to close, asynchronously
    /// </summary>
    /// <param name="frameFormat">FFmpeg frame foramt</param>
    /// <param name="seconds">Amount of seconds</param>
    public async Task ExtractEverySecondsWaitAsync(string frameFormat, int seconds)
    {
        await Video.ExtractAllFramesEverySecondsWaitAsync(Path, frameFormat, seconds);
    }

    /// <summary>
    /// Extracts frames from this video in an interval of custom amount of frames
    /// </summary>
    /// <param name="frameFormat">FFmpeg frame format</param>
    /// <param name="seconds">Amount of seconds</param>
    public void ExtractEveryFrames(string frameFormat, int seconds)
    {
        Video.ExtractAllFramesEveryFrames(Path, frameFormat, seconds);
    }

    /// <summary>
    /// Extracts frames from this video in an interval of custom amount of frames,
    /// and waits for ffmpeg to close
    /// </summary>
    /// <param name="frameFormat">FFmpeg frame format</param>
    /// <param name="seconds">Amount of seconds</param>
    public void ExtractEveryFramesWait(string frameFormat, int seconds)
    {
        Video.ExtractAllFramesEveryFramesWait(Path, frameFormat, seconds);
    }

    /// <summary>
    /// Extracts frames from this video in an interval of custom amount of frames,
    /// and waits for ffmpeg to close, asynchronously
    /// </summary>
    /// <param name="frameFormat">FFmpeg frame format</param>
    /// <param name="seconds">Amount of seconds</param>
    public async Task ExtractEveryFramesWaitAsync(string frameFormat, int seconds)
    {
        await Video.ExtractAllFramesEveryFramesWaitAsync(Path, frameFormat, seconds);
    }
}
