using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Configuration;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Constants;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;

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

    public async Task<List<IDomainEntity>> GetUpdatedEntitiesAsync<TDomainEntity>(UpdatedEntitiesQuery query) where TDomainEntity : IDomainEntity
    {
	    return typeof(TDomainEntity) switch
	    {
		    _ when typeof(TDomainEntity) == typeof(Category)
			    => await UpdatedEntitiesNotionDatabaseQueryAsync<NotionCategoryPageDto>(_settings.Databases.CategoriesDatabaseId, query.LastSync),
		    _ when typeof(TDomainEntity) == typeof(Playlist)
			    => await UpdatedEntitiesNotionDatabaseQueryAsync<NotionPlaylistPageDto>(_settings.Databases.PlaylistsDatabaseId, query.LastSync),
		    _ when typeof(TDomainEntity) == typeof(Post)
			    => await UpdatedEntitiesNotionDatabaseQueryAsync<NotionPostPageDto>(_settings.Databases.PostsDatabaseId, query.LastSync),
		    _
			    => throw new Exception()
	    };
    }

    private async Task<List<IDomainEntity>> UpdatedEntitiesNotionDatabaseQueryAsync<T>(string databaseId, LastSync lastSync) where T : NotionPageDto
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
}