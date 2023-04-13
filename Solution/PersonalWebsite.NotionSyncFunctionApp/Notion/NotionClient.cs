using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Exceptions;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

internal class NotionClient : INotionClient
{
	private readonly HttpClient _httpClient;
	private readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);

	public NotionClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<NotionListResponse<T>> QueryDatabaseAsync<T>(string notionDatabaseId, NotionQueryDatabaseBodyParameters bodyParameters) where T : NotionPageDto
	{
		try
		{
			var responseMessage = await _httpClient.PostAsJsonAsync(
				$"v1/databases/{notionDatabaseId}/query",
				bodyParameters,
				_jsonSerializerOptions);

			if (!responseMessage.IsSuccessStatusCode)
			{
				throw new NotionClientUnsuccessfulRequestException($"Querying Notion database {notionDatabaseId} failed with status code {responseMessage.StatusCode}");
			}

			var responseContent = await responseMessage.Content.ReadAsStringAsync();

			return JsonSerializer.Deserialize<NotionListResponse<T>>(responseContent, _jsonSerializerOptions) 
			       ?? throw new Exception($"{nameof(QueryDatabaseAsync)} unable to deserialize {responseContent} into {typeof(NotionListResponse<T>).Name}. Deserialization returned null.");
		}
		catch (Exception ex)
		{
			throw new NotionClientUnsuccessfulRequestException($"Querying Notion database {notionDatabaseId} was unsuccessful", ex);
		}
	}

	public async Task<NotionListResponse<T>> RetrieveBlockChildrenAsync<T>(string blockId) where T : NotionBlockDto
	{
		try
		{
			var responseMessage = await _httpClient.GetAsync(new Uri($"v1/blocks/{blockId}/children"));

			if (responseMessage.IsSuccessStatusCode == false)
			{
				throw new NotionClientUnsuccessfulRequestException($"Failed to retrieve the children for the page or block with Id: {blockId}, failed with status code {responseMessage.StatusCode}");
			}

			var responseContent = await responseMessage.Content.ReadAsStringAsync();

			return JsonSerializer.Deserialize<NotionListResponse<T>>(responseContent, _jsonSerializerOptions)
			       ?? throw new Exception($"{nameof(RetrieveBlockChildrenAsync)} unable to deserialize {responseContent} into {typeof(NotionListResponse<T>).Name}. Deserialization returned null.");
		}
		catch (Exception ex)
		{
			throw new NotionClientUnsuccessfulRequestException($"Querying Notion page {blockId} for block children was unsuccessful", ex);
		}
	}
}