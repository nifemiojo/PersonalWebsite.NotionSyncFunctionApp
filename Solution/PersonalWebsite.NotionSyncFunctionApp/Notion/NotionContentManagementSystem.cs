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
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

class NotionContentManagementSystem : IContentManagementSystem
{
    private readonly INotionClient _notionClient;
    private readonly NotionOptions _settings;

    public NotionContentManagementSystem(INotionClient notionClient, IOptions<NotionOptions> options)
    {
        _notionClient = notionClient;
		_settings = options.Value;
    }

    public async Task<List<IDomainEntity>> GetUpdatedEntitiesAsync<TDomainEntity>(LastSync lastSync) where TDomainEntity : IDomainEntity
    {
	    return typeof(TDomainEntity) switch
	    {
		    _ when typeof(TDomainEntity) == typeof(Category)
			    => await GetUpdatedEntitiesPagePropertiesAsync<NotionCategoryPage>(_settings.Databases.CategoriesDatabaseId, lastSync),
		    _ when typeof(TDomainEntity) == typeof(Playlist)
			    => await GetUpdatedEntitiesPagePropertiesAsync<NotionPlaylistPage>(_settings.Databases.PlaylistsDatabaseId, lastSync),
		    _ when typeof(TDomainEntity) == typeof(Post)
			    => await GetUpdatedEntitiesFullPageAsync<NotionPostPage>(_settings.Databases.PostsDatabaseId, lastSync, new NotionPostPageModel()),
		    _
			    => throw new Exception()
	    };
    }

    private async Task<List<IDomainEntity>> GetUpdatedEntitiesPagePropertiesAsync<T>(string databaseId, LastSync lastSync) where T : NotionPage
    {
	    var res = await _notionClient.QueryDatabaseAsync<T>(databaseId,
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

	    return res.Results
		    .Select(x => x.Map())
		    .ToList();
    }
    
    private async Task<List<IDomainEntity>> GetUpdatedEntitiesFullPageAsync<T>(string databaseId, LastSync lastSync, NotionPageModel pageModel) where T : NotionPage
    {
	    var res = await _notionClient.QueryDatabaseAsync<T>(databaseId,
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

	    var pageProperties = res.Results;

	    var entities = new List<IDomainEntity>();

		foreach (var notionPageDto in pageProperties)
		{
			pageModel.Properties = notionPageDto;

			var blocks = await _notionClient.RetrieveBlockChildrenAsync(notionPageDto.Id);
			pageModel.Content = blocks.Results;

		    entities.Add(pageModel.Map());
		}

		return entities;
    }
}