﻿using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

public interface INotionClient
{
	Task<NotionListResponse<T>> QueryDatabaseAsync<T>(string notionDatabaseId, NotionQueryDatabaseBodyParameters bodyParameters) where T : NotionPageDto;

	Task<NotionListResponse<T>> RetrieveBlockChildrenAsync<T>(string blockId) where T : NotionBlockDto;
}