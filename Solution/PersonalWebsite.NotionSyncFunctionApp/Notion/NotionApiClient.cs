using System.Net.Http;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

internal class NotionApiClient : INotionApiClient
{
	private readonly HttpClient _httpClient;

	public NotionApiClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<NotionListResponse<T>> QueryDatabaseAsync<T>(string databaseId, NotionDatabaseQuery queryBody) where T : NotionPageDto
	{
		throw new System.NotImplementedException();
	}

	public async Task<NotionListResponse<T>> RetrieveBlockChildrenAsync<T>(string blockId) where T : NotionBlockDto
	{
		throw new System.NotImplementedException();
	}
}