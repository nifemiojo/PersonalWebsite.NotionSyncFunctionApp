using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure;

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

        var actualLastSync = await sut.Retrieve();

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
        contentProperty!.SetValue(blobDownloadResult, BinaryData.FromString("2022-02-25T18:03:44"));

		response.Value.Returns(blobDownloadResult);
		lastSyncTimestampBlobClient
	        .DownloadContentAsync()
	        .Returns(response);

		var sut = new LastSyncTimestampAzureBlob(lastSyncTimestampBlobClient);

		var actualLastSync = await sut.Retrieve();

		actualLastSync.Timestamp.ToString().Should().Be("2022-02-25T18:03:44");
    }
}