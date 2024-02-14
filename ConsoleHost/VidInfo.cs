using System.Text;
using VideoSharp;

namespace ConsoleHost;

public static class VidInfo
{
    public static string HumanReadable(string videofile)
    {
        var cxt = new VideoContext(videofile);
        cxt.SyncInit();

        StringBuilder sb = new();

        sb.AppendLine($"Dimensions: {cxt.Dimension.Width}x{cxt.Dimension.Height}");
        sb.AppendLine($"Frame count: {cxt.FrameCount}");
        sb.AppendLine($"Frames per second: {cxt.Fps}");

        return sb.ToString();
    }
}
