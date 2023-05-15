using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Exceptions;

namespace PersonalWebsite.NotionSyncFunctionApp.UnitTests.Infrastructure;

[TestFixture]
internal class LastSyncTimestampAzureBlobTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    [SetUp]
    public void PerTestSetUp()
    {
    }

    [Test]
    public async Task Retrieve_ReturnsNoPreviousSyncType_WhenBlobDoesNotExist()
    {
        var lastSyncTimestampBlobClient = Substitute.For<BlobClient>();
        var falseResponse = Substitute.For<Azure.Response<bool>>();
        falseResponse.Value.Returns(false);
        lastSyncTimestampBlobClient.ExistsAsync().Returns(falseResponse);

        var sut = new LastSyncTimestampAzureBlob(lastSyncTimestampBlobClient);

        var actualLastSync = await sut.RetrieveAsync();

        actualLastSync.Should().BeOfType<NoPreviousLastSync>();
    }

    [Test]
    public async Task Retrieve_ReturnsSameTimestampAsBlob_WhenBlobExists()
    {
		var lastSyncTimestampBlobClient = Substitute.For<BlobClient>();

		var trueResponse = Substitute.For<Azure.Response<bool>>();
		trueResponse.Value.Returns(true);
		lastSyncTimestampBlobClient
			.ExistsAsync()
			.Returns(trueResponse);

		var response = Substitute.For<Azure.Response<BlobDownloadResult>>();
		var blobDownloadResult = (BlobDownloadResult) Activator.CreateInstance(typeof(BlobDownloadResult), true)!;
        var contentProperty = blobDownloadResult.GetType().GetProperty("Content");
        contentProperty!.SetValue(blobDownloadResult, BinaryData.FromString("2023-03-02T04:27:00.55Z"));

		response.Value.Returns(blobDownloadResult);
		lastSyncTimestampBlobClient
	        .DownloadContentAsync()
	        .Returns(response);

		var sut = new LastSyncTimestampAzureBlob(lastSyncTimestampBlobClient);

		var actualLastSync = await sut.RetrieveAsync();

		actualLastSync.Timestamp.Value.Should().Be("2023-03-02T04:27:00.55Z");
    }

    [Test]
    public async Task Retrieve_ThrowsBlobClientRequestException_WhenBlobClientThrowsRequestFailedException()
    {
		var lastSyncTimestampBlobClient = Substitute.For<BlobClient>();

		var trueResponse = Substitute.For<Azure.Response<bool>>();
		trueResponse.Value.Returns(true);
		lastSyncTimestampBlobClient.ExistsAsync().Returns(trueResponse);

		lastSyncTimestampBlobClient.DownloadContentAsync().Throws(new RequestFailedException("This is the Azure message"));

		var sut = new LastSyncTimestampAzureBlob(lastSyncTimestampBlobClient);

		Func<Task> act = async () => await sut.RetrieveAsync();

		await act.Should().ThrowAsync<BlobClientRequestException>();
	}
}