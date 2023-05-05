using System;
using System.IO;
using System.Threading.Tasks;

namespace PersonalWebsite.NotionSyncFunctionApp.Application;

public interface IAzureBlobContainer
{
	Task<Uri> UploadBlobAsync(string blobName, Stream blobStream);
}