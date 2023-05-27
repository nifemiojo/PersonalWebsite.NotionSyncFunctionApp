using Dapper;
using System.Data;
using System.Text.Json;
using Npgsql;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;
using NpgsqlTypes;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;

public class CategoryDtoData
{
	public string NotionId { get; set; }
	public string Name { get; set; }
}

public class PersonalWebsiteDatabase : IDatabase
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

    public PersonalWebsiteDatabase(IDatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public async Task UpsertCategoriesStoredProcedureAsync(List<CategoryDto> categoryDtos)
    {
	    var categoriesJson = JsonSerializer.Serialize(categoryDtos);

		/*var dynamicParams = new DynamicParameters();
		dynamicParams.Add("categories", categoriesJson, DbType.Object, ParameterDirection.Input);*/

		var connection = _databaseConnectionFactory.GetConnection();

		await connection.OpenAsync();

		using (var command = new NpgsqlCommand("blog.upsert_categories", (NpgsqlConnection)connection))
		{
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.AddWithValue("categories", NpgsqlDbType.Jsonb, categoriesJson);

			await command.ExecuteNonQueryAsync();
		}

		// await connection.ExecuteAsync("blog.upsert_categories", dynamicParams, commandType: CommandType.StoredProcedure);
    }

    public async Task UpsertPlaylistsStoredProcedureAsync(List<PlaylistDto> playlistDtos)
    {
	    var parameters = new
	    {
		    playlists = new
		    {
			    playlists = playlistDtos.Select(playlistDto => new
			    {
				    name = playlistDto.Name,
				    description = playlistDto.Description,
				    notion_entity_id = playlistDto.NotionId,
				    category_notion_entity_id = playlistDto.CategoryNotionId,
				    post_notion_entity_ids = playlistDto.PostNotionEntityIds
			    })
		    }
	    };

		await using var connection = _databaseConnectionFactory.GetConnection();

		await connection.ExecuteAsync("blog.upsert_playlists", parameters, commandType: CommandType.StoredProcedure);
	}

    public async Task UpsertPostsStoredProcedureAsync(List<PostDto> postDtos)
    {
		var parameters = new
		{
			posts = postDtos.Select(postDto => new
			{
				description = postDto.Description,
				html = postDto.Html,
				title = postDto.Title,
				notion_entity_id = postDto.NotionId,
				playlist_notion_entity_ids = postDto.PlaylistNotionIds
			})
		};

		await using var connection = _databaseConnectionFactory.GetConnection();

	    await connection.ExecuteAsync("blog.upsert_posts", parameters, commandType: CommandType.StoredProcedure);

	}
}