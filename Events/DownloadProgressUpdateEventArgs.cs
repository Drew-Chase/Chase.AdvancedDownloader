
namespace Chase.AdvancedDownloader.Events;

public class DownloadProgressUpdateEventArgs : EventArgs
{
    public float Percentage { get; }
    public long BytesRemaining { get; }
    public long BytesDownloaded { get; }
    public long TotalBytes { get; }
    public long BytesPerSecond { get; }

    internal DownloadProgressUpdateEventArgs(long bytesDownloaded, long totalBytes, long bytesPerSecond)
    {
        BytesDownloaded = bytesDownloaded;
        TotalBytes = totalBytes;
        BytesPerSecond = bytesPerSecond;
        Percentage = bytesDownloaded / totalBytes;
        BytesRemaining = bytesDownloaded - totalBytes;
    }
}
