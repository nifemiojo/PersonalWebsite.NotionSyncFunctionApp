using Dapper;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;

internal interface IDatabase
{
    Task UpsertCategoriesStoredProcedureAsync(List<CategoryDto> categoryDtos);
    Task UpsertPlaylistsStoredProcedureAsync(List<PlaylistDto> playlistDtos);
    Task UpsertPostsStoredProcedureAsync(List<PostDto> postDtos);
}