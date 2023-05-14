using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Mapping;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

class BlogEntityEntityRepository : IBlogEntityRepository
{
    private readonly IDatabase _database;

    public BlogEntityEntityRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task UpsertAsync<T>(List<T> entitiesToUpsert) where T : BlogEntity
    {
	    var dtosToUpsert = entitiesToUpsert.Select(blogEntity => BlogEntityDto.Map(blogEntity)).ToList();

	    if (typeof(T) == typeof(Category))
	    {
		    await _database.UpsertCategoriesStoredProcedureAsync(dtosToUpsert.Cast<CategoryDto>().ToList());
	    }
	    else if (typeof(T) == typeof(Playlist))
	    {
		    await _database.UpsertPlaylistsStoredProcedureAsync(dtosToUpsert.Cast<PlaylistDto>().ToList());
	    }
	    else if (typeof(T) == typeof(Post))
	    {
		    await _database.UpsertPostsStoredProcedureAsync(dtosToUpsert.Cast<PostDto>().ToList());
	    }
	    else
	    {
		    throw new NotImplementedException();
	    }
    }
}