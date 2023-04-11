using System;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.NotionSyncFunctionApp;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure;
using PersonalWebsite.NotionSyncFunctionApp.Notion;

[assembly: FunctionsStartup(typeof(Startup))]

namespace PersonalWebsite.NotionSyncFunctionApp;

public class Startup : FunctionsStartup
{
	public override void Configure(IFunctionsHostBuilder builder)
	{
		var storageAccountName = builder.GetContext().Configuration.GetValue<string>("AzureWebJobsStorage:accountName");
		var storageContainerName = builder.GetContext().Configuration.GetValue<string>("AzureWebJobsStorage:containerName");
		var storageBlobName = builder.GetContext().Configuration.GetValue<string>("AzureWebJobsStorage:blobName");

		builder.Services.AddAzureClients(clientBuilder =>
		{
			clientBuilder.AddClient<BlobClient, BlobClientOptions>(
				_ => new BlobClient(
					new Uri($"https://{storageAccountName}.blob.core.windows.net/{storageContainerName}/{storageBlobName}"),
					new DefaultAzureCredential()));
		});

		builder.Services.AddSingleton<ILastSyncTimestampStorage, LastSyncTimestampAzureBlob>();

		builder.Services.AddHttpClient<NotionApiClient>(httpClient =>
		{
			httpClient.BaseAddress = new Uri("https://api.notion.com/");
			httpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
			httpClient.Timeout = TimeSpan.FromSeconds(3);
		});

		// builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
	}
}
