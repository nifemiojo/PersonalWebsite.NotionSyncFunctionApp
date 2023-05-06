using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Client;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Configuration;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Constants;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

class NotionContentManagementSystem : IContentManagementSystem
{
    private readonly INotionClient _notionClient;
    private readonly INotionConversion _notionConversion;
    private readonly NotionOptions _settings;

    public NotionContentManagementSystem(
	    INotionClient notionClient,
	    IOptions<NotionOptions> options,
	    INotionConversion notionConversion)
    {
        _notionClient = notionClient;
        _notionConversion = notionConversion;
        _settings = options.Value;
    }

    public async Task<List<IDomainEntity>> GetUpdatedAsync<TDomainEntity>(LastSync lastSync) where TDomainEntity : IDomainEntity
    {
	    return typeof(TDomainEntity) switch
	    {
		    _ when typeof(TDomainEntity) == typeof(Category)
			    => await GetUpdatedPagesAsync<NotionCategoryPage>(_settings.Databases.CategoriesDatabaseId, lastSync),
		    _ when typeof(TDomainEntity) == typeof(Playlist)
			    => await GetUpdatedPagesAsync<NotionPlaylistPage>(_settings.Databases.PlaylistsDatabaseId, lastSync),
		    _ when typeof(TDomainEntity) == typeof(Post)
			    => await GetUpdatedPagesWithBlocksAsync<NotionPostPage>(_settings.Databases.PostsDatabaseId, lastSync),
		    _
			    => throw new Exception()
	    };
    }

    private async Task<List<IDomainEntity>> GetUpdatedPagesAsync<T>(string databaseId, LastSync lastSync) where T : NotionPage
    {
	    var paginatedResponse = await GetPagesDatabaseQueryAsync<T>(databaseId, lastSync);

	    return MapPagesToDomainEntities(paginatedResponse.Results);
    }

    private async Task<List<IDomainEntity>> GetUpdatedPagesWithBlocksAsync<T>(string databaseId, LastSync lastSync) where T : NotionPostPage
    {
	    var paginatedResponse = await GetPagesDatabaseQueryAsync<T>(databaseId, lastSync);

	    var entities = new List<IDomainEntity>();

		foreach (var page in paginatedResponse.Results)
		{
			var domainEntity = GetContentBasedEntity(typeof(T));

			var paginatedResponseBlocks = await GetChildBlocksAsync(page.Id);
			foreach (var notionBlock in paginatedResponseBlocks.Results)
			{
				await NestChildBlocks<T>(notionBlock);
			}

			var html = _notionConversion.ConvertNotionPostToHtmlString(page.Properties.Title.Title.Single().PlainText, paginatedResponseBlocks.Results);

			domainEntity.Content = new PostContent { Html = html };

			entities.Add(domainEntity);
		}

		return entities;
    }

    private async Task NestChildBlocks<T>(NotionBlock notionBlock) where T : NotionPage
    {
	    if (notionBlock.HasChildren)
	    {
		    var childBlocks = await GetChildBlocksAsync(notionBlock.Id);

		    foreach (var childBlock in childBlocks.Results)
		    {
			    notionBlock.ChildBlocks.Add(childBlock);
			    await NestChildBlocks<T>(childBlock);
		    }
	    }
    }

    private ContentBasedEntity GetContentBasedEntity(Type type)
    {
	    return type switch
	    {
		    _ when type == typeof(NotionPostPage)
			    => new Post(),
		    _
			    => throw new Exception()
	    };
    }

    private async Task<NotionPaginatedResponse<T>> GetPagesDatabaseQueryAsync<T>(string databaseId, LastSync lastSync) where T : NotionPage
    {
	    return await _notionClient.QueryDatabaseAsync<T>(databaseId,
		    new NotionQueryDatabaseBodyParameters
		    {
			    Filter = new NotionFilter
			    {
				    Property = NotionDatabaseConstants.LastEditedProperty,
				    Date = new NotionDateFilter
				    {
					    OnOrAfter = lastSync.Timestamp.Value
				    }
			    }
		    });
    }

    private async Task<NotionPaginatedResponse<NotionBlock>> GetChildBlocksAsync(string pageId)
    {
	    return await _notionClient.RetrieveBlockChildrenAsync(pageId);
    }

    private static List<IDomainEntity> MapPagesToDomainEntities<T>(List<T> pages) where T : NotionPage
    {
	    return pages
		    .Select(notionPage => notionPage.MapToDomain())
		    .ToList();
    }
}