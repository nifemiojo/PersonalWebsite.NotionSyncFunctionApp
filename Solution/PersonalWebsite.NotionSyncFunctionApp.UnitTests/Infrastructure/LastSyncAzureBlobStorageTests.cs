using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using PersonalWebsite.ContentSyncFunction;
using PersonalWebsite.ContentSyncFunction.Common;

namespace PersonalWebsite.ContentSync.UnitTests.Infrastructure;

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
    public async Task Retrieve_ReturnsIsFirstSyncTrue_WhenBlobDoesNotExist()
    {
        var lastSyncTimestampBlobClient = Substitute.For<BlobClient>();
        var falseResponse = Substitute.For<Azure.Response<bool>>();
        falseResponse.Value.Returns(false);
        lastSyncTimestampBlobClient.ExistsAsync().Returns(falseResponse);

        var sut = new LastSyncTimestampAzureBlob(lastSyncTimestampBlobClient);

        var actualLastSync = await sut.Retrieve();

        actualLastSync.IsFirstSync.Should().BeTrue();
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
		var downloadResult = Substitute.For<BlobDownloadResult>();
        downloadResult.Content.Returns(BinaryData.FromString("2022-02-25T18:03:44"));
		response.Value.Returns(downloadResult);
		lastSyncTimestampBlobClient
	        .DownloadContentAsync()
	        .Returns(response);

		var sut = new LastSyncTimestampAzureBlob(lastSyncTimestampBlobClient);

		var actualLastSync = await sut.Retrieve();

		actualLastSync.Timestamp.Should().Be(Iso8601DateTime.FromString("2022-02-25T18:03:44"));
    }
}