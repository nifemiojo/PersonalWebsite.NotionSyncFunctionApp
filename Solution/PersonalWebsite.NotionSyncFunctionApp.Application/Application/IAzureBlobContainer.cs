namespace PersonalWebsite.NotionSyncFunctionApp.Application.Application;

public interface IAzureBlobContainer
{
	Task<Uri> UploadBlobAsync(string blobName, Stream blobStream);
}