using Chase.AdvancedDownloader.Events;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Timer = System.Timers.Timer;

namespace Chase.AdvancedDownloader;

public static class DownloadManager
{
    public static void Download(string url, string directory, string filename, int parts, EventHandler<DownloadProgressUpdateEventArgs> update_event)
    {
        if (TryGetHeaders(url, out HttpContentHeaders? headers) && headers?.ContentLength != null)
        {
            long size = headers.ContentLength.Value;
            DownloadChunk[] chunks = GetChunks(parts, size);
            Dictionary<int, DownloadProgressUpdateEventArgs> updates = new();
            Timer timer = new(1000)
            {
                Enabled = true,
                AutoReset = true,
            };
            long bytesDownloaded = 0;
            long lastByteCheck = 0;
            timer.Elapsed += (s, e) =>
            {
                update_event.Invoke(null, new(bytesDownloaded, size, lastByteCheck - bytesDownloaded));
                lastByteCheck = bytesDownloaded;
            };
            timer.Start();

            Timer chunk_timer = new(500)
            {
                Enabled = true,
                AutoReset = true,
            };
            chunk_timer.Start();
            for (int i = 0; i < chunks.Length; i++)
            {
                DownloadChunk chunk = chunks[i];

                chunk_timer.Elapsed += (s, e) =>
                {
                    chunk.
                };
            }
        }
    }

    private static DownloadChunk[] GetChunks(int count, long totalSize)
    {
        DownloadChunk[] chunks = new DownloadChunk[count];
        long chunkSize = totalSize / count;

        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i] = new()
            {
                Start = i == 0 ? 0 : chunkSize * i + 1,
                End = i == 0 ? chunkSize : i == chunks.Length - 1 ? totalSize : chunkSize * i + chunkSize
            };
        }
        return chunks;
    }

    private static bool TryGetHeaders(string url, out HttpContentHeaders? headers)
    {
        headers = null;
        using (HttpClient client = new())
        {
            using HttpRequestMessage message = new(HttpMethod.Head, url);
            using HttpResponseMessage response = client.Send(message);
            if (response.IsSuccessStatusCode)
            {
                headers = response.Content.Headers;
            }
        }

        return headers != null;
    }
}

