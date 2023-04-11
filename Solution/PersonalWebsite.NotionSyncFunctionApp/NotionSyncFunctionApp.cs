using System;
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
using PersonalWebsite.NotionSyncFunctionApp.Application;

namespace PersonalWebsite.NotionSyncFunctionApp
{
    public class NotionSyncFunctionApp
    {
        private readonly ILogger<NotionSyncFunctionApp> _logger;
        private readonly ILastSyncTimestampStorage _lastSyncTimestampStorage;

        public NotionSyncFunctionApp(ILogger<NotionSyncFunctionApp> log,
	        ILastSyncTimestampStorage lastSyncTimestampStorage)
        {
	        _logger = log;
	        _lastSyncTimestampStorage = lastSyncTimestampStorage;
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
                var lastSync = await _lastSyncTimestampStorage.Retrieve();
                await _lastSyncTimestampStorage.Upsert();

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
}

