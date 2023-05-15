using System;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Application.Application;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Exceptions;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure;

public class LastSyncTimestampAzureBlob : ILastSyncTimestampStorage
{
    private readonly BlobClient _lastSyncBlobClient;

    public LastSyncTimestampAzureBlob(BlobClient lastSyncBlobClient)
    {
        _lastSyncBlobClient = lastSyncBlobClient;
    }

    public async Task<LastSync> RetrieveAsync()
    {
	    try
	    {
	        if (!(await _lastSyncBlobClient.ExistsAsync()).Value)
	        {
	            return new NoPreviousLastSync();
	        }

	        var blobDownloadResult = await _lastSyncBlobClient.DownloadContentAsync();
	        var lastSyncTimestamp = blobDownloadResult.Value.Content.ToString();

	        return new LastSync(lastSyncTimestamp);
	    }
	    catch (RequestFailedException requestFailedException)
	    {
		    throw new BlobClientRequestException($"Error trying to retrieve the last sync timestamp blob from: {_lastSyncBlobClient.Uri}.", requestFailedException);
	    }
    }

    public async Task UpsertAsync()
    {
	    try
	    {
	        var formattedDateTime = Iso8601FormattedDateTime.CreateFrom(DateTime.UtcNow);
			await _lastSyncBlobClient.UploadAsync(BinaryData.FromString(formattedDateTime.Value), overwrite: true);
	    }
	    catch (RequestFailedException requestFailedException)
	    {
		    throw new BlobClientRequestException($"Error trying to upsert the last sync timestamp blob to: {_lastSyncBlobClient.Uri}.", requestFailedException);
	    }
	}
}