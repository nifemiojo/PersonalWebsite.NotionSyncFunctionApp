using System;
using System.Globalization;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace PersonalWebsite.ContentSyncFunction;

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
		DateTime lastSyncTimestamp;
		try
		{
			if (!(await _lastSyncBlobClient.ExistsAsync()).Value)
			{
				// Set lastSyncTimestamp to the oldest possible DateTime
				lastSyncTimestamp = DateTime.MinValue;
			}
			else
			{
				var blobDownloadResult = await _lastSyncBlobClient.DownloadContentAsync();
				lastSyncTimestamp = DateTime.Now;
				//lastSyncTimestamp = blobDownloadResult.Value.Content.ToString();
			}
		}
		catch (Exception e)
		{
			// _logger.LogError(e, "Unable to retrieve lastsynctimestamp.");
			Console.WriteLine(e);
			throw;
		}

		return new LastSync(lastSyncTimestamp);
	}
}

public interface ILastSyncTimestampStorage
{
	Task<LastSync> Retrieve();
}