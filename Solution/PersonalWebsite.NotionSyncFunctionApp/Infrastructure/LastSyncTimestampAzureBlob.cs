using System.Threading.Tasks;
using Azure.Storage.Blobs;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure;

public class LastSyncTimestampAzureBlob : ILastSyncTimestampStorage
{
    private readonly BlobClient _lastSyncBlobClient;

    public LastSyncTimestampAzureBlob(BlobClient lastSyncBlobClient)
    {
        _lastSyncBlobClient = lastSyncBlobClient;
    }

    public async Task<LastSync> Retrieve()
    {
        // Get the last sync timestamp from blob storage as a string
        if (!(await _lastSyncBlobClient.ExistsAsync()).Value)
        {
            // Set lastSyncTimestamp to the oldest possible DateTime
            return new NoPreviousLastSync();
        }

        var blobDownloadResult = await _lastSyncBlobClient.DownloadContentAsync();
        var lastSyncTimestamp = blobDownloadResult.Value.Content.ToString();

        return new LastSync(lastSyncTimestamp);
    }
}