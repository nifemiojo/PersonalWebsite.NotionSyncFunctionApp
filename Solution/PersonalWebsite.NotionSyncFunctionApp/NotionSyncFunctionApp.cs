using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp;

public class NotionSyncFunctionApp
{
	private readonly ILastSyncTimestampStorage _lastSyncTimestampStorage;
	private readonly IContentManagementSystem _contentManagementSystem;
	private readonly IBlogRepository _blogRepository;

	public NotionSyncFunctionApp(ILastSyncTimestampStorage lastSyncTimestampStorage,
		IContentManagementSystem contentManagementSystem)
	{
		_lastSyncTimestampStorage = lastSyncTimestampStorage;
		_contentManagementSystem = contentManagementSystem;
	}

	[FunctionName("sync")]
	[OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
	[OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
	[OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
	public async Task<IActionResult> Run(
		[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest request)
	{
		try
		{
			var lastSync = await _lastSyncTimestampStorage.RetrieveAsync();

			var categories = await _contentManagementSystem.GetUpdatedBlogEntitiesAsync<Category>(lastSync);
			var playlists = await _contentManagementSystem.GetUpdatedBlogEntitiesAsync<Playlist>(lastSync);
			var posts = await _contentManagementSystem.GetUpdatedBlogEntitiesAsync<Post>(lastSync);

			await _blogRepository.UpsertAsync<Category>(categories);
			await _blogRepository.UpsertAsync<Playlist>(playlists);
			await _blogRepository.UpsertAsync<Post>(posts);

			await _lastSyncTimestampStorage.UpsertAsync();

			return new OkObjectResult("Izz Okay");

		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}