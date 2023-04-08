using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PersonalWebsite.ContentSyncFunction.Common;

namespace PersonalWebsite.ContentSyncFunction
{
    public class ContentSyncFunction
    {
        private readonly ILogger<ContentSyncFunction> _logger;
        private readonly ILastSyncTimestampStorage _lastSyncTimestampStorage;

        public ContentSyncFunction(ILogger<ContentSyncFunction> log,
	        ILastSyncTimestampStorage lastSyncTimestampStorage)
        {
	        _logger = log;
	        _lastSyncTimestampStorage = lastSyncTimestampStorage;
        }

        [FunctionName("sync-content")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest request)
        {
	       // var lastSync = new LastSync(await _lastSyncTimestampStorage.Retrieve());

            //_logger.LogInformation($"Time: {lastSync.Timestamp}");

	        _logger.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult("Izz Okay");
        }
    }

    public class LastSync
    {
	    public Iso8601DateTime Timestamp { get; }

		public bool IsFirstSync => Timestamp.ToString() == Iso8601DateTime.FromDateTime(DateTime.MinValue).ToString();

        public LastSync(DateTime timestamp)
        {
	        Timestamp = Iso8601DateTime.FromDateTime(timestamp);
		}

        public LastSync(Iso8601DateTime timestamp)
        {
	        Timestamp = timestamp;
        }

        public LastSync(string timestamp)
        {
	        if (string.IsNullOrEmpty(timestamp))
	        {
				Timestamp = Iso8601DateTime.FromDateTime(DateTime.MinValue);
			}
	        else
	        {
				Timestamp = Iso8601DateTime.FromString(timestamp);
			}
		}
    }
}

