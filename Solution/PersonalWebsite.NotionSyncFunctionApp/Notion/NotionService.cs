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
using PersonalWebsite.ContentSyncFunction.Common;
using PersonalWebsite.ContentSyncFunction.Domain;
using PersonalWebsite.ContentSyncFunction.Notion.Exceptions;
using PersonalWebsite.ContentSyncFunction.Notion.Mapping;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Block;
using PersonalWebsite.ContentSyncFunction.Notion.Pages;
using PersonalWebsite.ContentSyncFunction.Notion.Request;
using PersonalWebsite.ContentSyncFunction.Notion.Response;

namespace PersonalWebsite.ContentSyncFunction.Notion;

internal class NotionService : INotionService
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions jsonSerializerOptions= new JsonSerializerOptions
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
        var lastSync = await GetLastExecutedSync();
        // For Testing: Iso8601DateTime lastSync = new Iso8601DateTime(new DateTime(2022, 01, 01));

        var queryBody = new NotionDatabaseQuery
        {
            Filter = new NotionFilter
            {
                Property = "Last Edited Time",
                Date = new NotionDateFilter
                {
                    OnOrAfter = lastSync.ToString()
                }
            }
        };

        string notionCategoryDatabaseId = "";
        string notionPlaylistDatabaseId = "";
        string notionPostDatabaseId = "";

        var categoryPages = await GetUpdatedPagesFromNotionDatabase<NotionCategoryPage>(notionCategoryDatabaseId, queryBody);
        var playlistPages = await GetUpdatedPagesFromNotionDatabase<NotionPlaylistPage>(notionPlaylistDatabaseId, queryBody);
        var postPages = await GetUpdatedPagesFromNotionDatabase<NotionPostPage>(notionPostDatabaseId, queryBody);

        // Get Page Content For Post Pages
        var postsContent = await GetPostsToBeUpdatedContent(postPages.Results.Select(notionPost => notionPost.Id).ToList());
        foreach (var pageContent in postsContent)
        {
	        string html = ConvertNotionPageContentToHtml(pageContent);
        }

	    // Map to Domain Objects
	    var categories = categoryPages.Results.Select(NotionMapper.Map);
	    var playlists = playlistPages.Results.Select(NotionMapper.Map);
	    var posts = postPages.Results.Select(NotionMapper.Map);

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
			var postBlocks =  await GetNotionListResponse<NotionBlock>(responseMessage);
            postsDictionary.Add(new NotionPageContent{PageId = postId, Content = postBlocks.Results});
	    }

        return postsDictionary;
	}

	private async Task<NotionListResponse<TNotionResponseType>> GetUpdatedPagesFromNotionDatabase<TNotionResponseType>(string notionDatabaseId, NotionDatabaseQuery query)
	    where TNotionResponseType : NotionPage
	{
        var responseMessage = await QueryNotionDatabase(notionDatabaseId, query);
        return await GetNotionListResponse<TNotionResponseType>(responseMessage);
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

    private async Task<Iso8601DateTime> GetLastExecutedSync()
    {
        // Uri should be in config and be split into account name + container name
        var blobClient = new BlobClient(new Uri("https://{account_name}.blob.core.windows.net/{container_name}/{blob_name}"),
            new DefaultAzureCredential());

        BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
        string downloadedData = downloadResult.Content.ToString();

        return Iso8601DateTime.FromString(downloadedData);
    }

    private async Task<HttpResponseMessage> QueryNotionDatabase(string notionDatabaseId, NotionDatabaseQuery query)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync(new Uri($"v1/databases/{notionDatabaseId}/query"),
            query, new JsonSerializerOptions());

        if (responseMessage.IsSuccessStatusCode == false)
        {
            throw new UnsuccessfulNotionRequest($"Querying Notion database {notionDatabaseId} failed with status code {responseMessage.StatusCode}");
        }

        return responseMessage;
    }
    
    private async Task<HttpResponseMessage> GetBlockChildren(string pageId)
    {
        var responseMessage = await _httpClient.GetAsync(new Uri($"v1/blocks/{pageId}/children"));

        if (responseMessage.IsSuccessStatusCode == false)
        {
            throw new UnsuccessfulNotionRequest($"Failed to retrieve the children for the page or block with Id: {pageId}, failed with status code {responseMessage.StatusCode}");
        }

        return responseMessage;
    }
}