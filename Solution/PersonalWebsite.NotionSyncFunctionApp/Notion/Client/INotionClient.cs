using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Client;

internal interface INotionClient
{
    Task<NotionPaginatedResponse<T>> QueryDatabaseAsync<T>(string notionDatabaseId, NotionQueryDatabaseBodyParameters bodyParameters) where T : NotionPage;

    Task<NotionPaginatedResponse<NotionBlock>> RetrieveBlockChildrenAsync(string blockId);
}