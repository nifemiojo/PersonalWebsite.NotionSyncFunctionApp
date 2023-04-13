using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Exceptions;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Mapping;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

internal class NotionService : INotionService
{
	private readonly HttpClient _httpClient;

	private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
	{
		PropertyNameCaseInsensitive = true,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	};

	public NotionService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<EditedContent> GetEditedNotionDatabaseContent()
	{

		var queryBody = new NotionQueryDatabaseBodyParameters
		{
			Filter = new NotionFilter
			{
				Property = "Last Edited Time",
				Date = new NotionDateFilter
				{
					OnOrAfter = ""
				}
			}
		};


		
		/*string notionCategoryDatabaseId = "";
		string notionPlaylistDatabaseId = "";
		string notionPostDatabaseId = "";

		// Get updated pages
		var categoryPages = await GetUpdatedPagesFromNotionDatabase<NotionCategoryPageDto>(notionCategoryDatabaseId, queryBody);
		var playlistPages = await GetUpdatedPagesFromNotionDatabase<NotionPlaylistPageDto>(notionPlaylistDatabaseId, queryBody);
		var postPages = await GetUpdatedPagesFromNotionDatabase<NotionPostPageDto>(notionPostDatabaseId, queryBody);

		// Map to Domain Objects
		var categories = categoryPages.Results.Select(NotionMapper.Map);
		var playlists = playlistPages.Results.Select(NotionMapper.Map);
		var posts = postPages.Results.Select(NotionMapper.Map);

		// Get Page Content For Post Pages
		var postsContent = await GetPostsToBeUpdatedContent(postPages.Results.Select(notionPost => notionPost.Id).ToList());
		foreach (var pageContent in postsContent)
		{
			string html = ConvertNotionPageContentToHtml(pageContent);
		}

		*/

		// Return
		return new EditedContent();
	}

	private string ConvertNotionPageContentToHtml(NotionPageContent pageContent)
	{
		throw new NotImplementedException();
	}

	private async Task<List<NotionPageContent>> GetPostsToBeUpdatedContent(List<string> postIds)
	{
		var postsDictionary = new List<NotionPageContent>();

		foreach (var postId in postIds)
		{
			var responseMessage = await GetBlockChildren(postId);
			var postBlocks = await GetNotionListResponse<NotionBlockDto>(responseMessage);
			postsDictionary.Add(new NotionPageContent { PageId = postId, Content = postBlocks.Results });
		}

		return postsDictionary;
	}

	private static async Task<NotionListResponse<TNotionResponseType>> GetNotionListResponse<TNotionResponseType>(HttpResponseMessage responseMessage)
	{
		try
		{
			var notionListResponse = await responseMessage.Content.ReadFromJsonAsync<NotionListResponse<TNotionResponseType>>();
			return notionListResponse;
		}
		catch (Exception ex)
		{
			throw new Exception("Deserializing Notion List Response of Notion Pages failed", ex);
		}
	}

	private async Task<HttpResponseMessage> GetBlockChildren(string pageId)
	{
		throw new NotImplementedException();
	}
}