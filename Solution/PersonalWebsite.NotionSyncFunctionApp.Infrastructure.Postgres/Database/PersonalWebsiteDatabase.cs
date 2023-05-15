using Dapper;
using System.Data;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;

public class PersonalWebsiteDatabase : IDatabase
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

    public PersonalWebsiteDatabase(IDatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public async Task UpsertCategoriesStoredProcedureAsync(List<CategoryDto> categoryDtos)
    {
        var parameters = new { rows_to_insert = categoryDtos.Select(x => new { x.NotionId, x.Name }).ToArray() };

        await using var connection = _databaseConnectionFactory.GetConnection();

        await connection.ExecuteAsync("blog.upsert_categories", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task UpsertPlaylistsStoredProcedureAsync(List<PlaylistDto> playlistDtos)
    {
		var parameters = new { rows_to_insert = playlistDtos.Select(x => new {  }).ToArray() };

		await using var connection = _databaseConnectionFactory.GetConnection();

		await connection.ExecuteAsync("blog.upsert_playlists", parameters, commandType: CommandType.StoredProcedure);
	}

    public async Task UpsertPostsStoredProcedureAsync(List<PostDto> postDtos)
    {
	    var parameters = new { rows_to_insert = postDtos.Select(x => new {  }).ToArray() };

	    await using var connection = _databaseConnectionFactory.GetConnection();

	    await connection.ExecuteAsync("blog.upsert_categories", parameters, commandType: CommandType.StoredProcedure);

	}
}