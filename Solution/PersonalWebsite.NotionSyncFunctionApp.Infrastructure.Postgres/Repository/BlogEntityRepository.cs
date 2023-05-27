using PersonalWebsite.NotionSyncFunctionApp.Application.Application;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

public class BlogEntityRepository : IBlogEntityRepository
{
    private readonly IDatabase _database;
    private readonly IBlogEntityToDtoMapper _blogEntityToDtoMapper;

    public BlogEntityRepository(IDatabase database, IBlogEntityToDtoMapper blogEntityToDtoMapper)
    {
	    _database = database;
	    _blogEntityToDtoMapper = blogEntityToDtoMapper;
    }

    public async Task UpsertAsync<TBlogEntity>(List<BlogEntity> entitiesToUpsert) where TBlogEntity : BlogEntity
    {
	    var blogEntityDtos = _blogEntityToDtoMapper.MapToDtos(entitiesToUpsert);

	    if (typeof(TBlogEntity) == typeof(Category))
	    {
		    await _database.UpsertCategoriesStoredProcedureAsync(blogEntityDtos.Cast<CategoryDto>().ToList());
	    }
	    else if (typeof(TBlogEntity) == typeof(Playlist))
	    {
		    await _database.UpsertPlaylistsStoredProcedureAsync(blogEntityDtos.Cast<PlaylistDto>().ToList());
	    }
	    else if (typeof(TBlogEntity) == typeof(Post))
	    {
		    await _database.UpsertPostsStoredProcedureAsync(blogEntityDtos.Cast<PostDto>().ToList());
	    }
	    else
	    {
		    throw new EntityNotSupportedForUpsertException();
	    }
    }
}