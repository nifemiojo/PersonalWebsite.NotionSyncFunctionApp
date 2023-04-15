using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Client;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;

namespace PersonalWebsite.NotionSyncFunctionApp;

public class NotionSyncFunctionApp
{
	private readonly ILogger<NotionSyncFunctionApp> _logger;
	private readonly INotionClient _notionClient;

	public NotionSyncFunctionApp(ILogger<NotionSyncFunctionApp> log,
		INotionClient notionClient)
	{
		_logger = log;
		_notionClient = notionClient;
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
			List<IBlogEntity> databasePages = getUpdatedNotionPagesQuery.Execute();
			updateBlogEntities.Update();

			List<NotionCategoryModel> queryResponse = getUpdatedNotionDatabasePages.Query(UpdatedNotionDatabasePagesRequest postDatabaseQuery);
			List<NotionCategoryModel> queryResponse = updatedNotionDatabasePagesQueryHandler.Query(UpdatedNotionDatabasePagesQueryRequest postDatabaseQuery);
			List<NotionCategoryModel> queryResponse = updatedNotionDatabasePagesQueryHandler.Query(UpdatedNotionDatabasePagesQueryRequest postDatabaseQuery);

			var response = await _notionClient.QueryDatabaseAsync<NotionCategoryPage>("66df5294a76a454b914bf659e1a41d41",
				new NotionQueryDatabaseBodyParameters
				{
					Filter = new NotionFilter
					{
						Property = "Last Edited Time",
						Date = new NotionDateFilter
						{
							OnOrAfter = "2022-02-25T18:03:44Z"
						}
					}
				});

			_logger.LogInformation("C# HTTP trigger function processed a request.");
			return new OkObjectResult("Izz Okay");

		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}