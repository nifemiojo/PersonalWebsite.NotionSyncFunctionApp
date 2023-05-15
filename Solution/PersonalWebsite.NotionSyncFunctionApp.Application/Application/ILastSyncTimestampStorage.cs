using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application.Application;

public interface ILastSyncTimestampStorage : IAzureBlob
{
    Task<LastSync> RetrieveAsync();

	/// <summary>
	/// Overwrites or creates the last sync timestamp blob.
	/// If the blob already exists, it will be overwritten with the current time.
	/// If the blob does not exist, it will be created with the current time.
	/// The timestamp is always UTC.
	/// </summary>
	Task UpsertAsync();
}