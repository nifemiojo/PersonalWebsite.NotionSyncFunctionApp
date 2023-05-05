using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace PersonalWebsite.NotionSyncFunctionApp.Application;

class BlogPostImagesAzureBlobContainer : IAzureBlobContainer
{
	private readonly BlobContainerClient _blobContainerClient;

	public BlogPostImagesAzureBlobContainer(BlobContainerClient blobContainerClient)
	{
		_blobContainerClient = blobContainerClient;
	}

	public async Task<Uri> UploadBlobAsync(string blobName, Stream blobStream)
	{
		var blobClient = _blobContainerClient.GetBlobClient(blobName);

		if (!await blobClient.ExistsAsync())
			await blobClient.UploadAsync(blobStream, overwrite: false);

		return blobClient.Uri;
	}
}