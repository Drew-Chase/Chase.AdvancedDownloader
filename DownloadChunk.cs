namespace Chase.AdvancedDownloader;

internal struct DownloadChunk
{
    public long BytesDownloaded;
    public long BytesPerSecond;
    public long End;
    public long Start;
    public long Size => End - Start;
}
