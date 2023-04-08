using System;
using System.IO;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.NotionSyncFunctionApp;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure;

[assembly: FunctionsStartup(typeof(Startup))]

namespace PersonalWebsite.NotionSyncFunctionApp;

public class Startup : FunctionsStartup
{
	public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
	{
		FunctionsHostBuilderContext context = builder.GetContext();

		if (context.EnvironmentName == "Development")
		{
			builder.ConfigurationBuilder
				.AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), optional: true,
					reloadOnChange: false);
		}
	}

	public override void Configure(IFunctionsHostBuilder builder)
	{
		var storageAccountName = builder.GetContext().Configuration.GetValue<string>("Values:AzureWebJobsStorage__accountName");
		var storageContainerName = builder.GetContext().Configuration.GetValue<string>("Values:AzureWebJobsStorage__containerName");
		var storageBlobName = builder.GetContext().Configuration.GetValue<string>("Values:AzureWebJobsStorage__blobName");

		builder.Services.AddAzureClients(clientBuilder =>
		{
			clientBuilder.UseCredential(new DefaultAzureCredential());

			clientBuilder.AddClient<BlobClient, BlobClientOptions>(
				_ => new BlobClient(new Uri($"https://{storageAccountName}.blob.core.windows.net/{storageContainerName}/{storageBlobName}")))
				.WithName("LastSyncTimestampBlobClient");
		});

		builder.Services.AddSingleton<ILastSyncTimestampStorage, LastSyncTimestampAzureBlob>();

		/*builder.Services.AddHttpClient<NotionService>(httpClient =>
		{
			httpClient.BaseAddress = new Uri("https://api.notion.com/");
			httpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
			httpClient.Timeout = TimeSpan.FromSeconds(3);
		});*/

		// builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
	}
}
