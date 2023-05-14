using Dapper;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;

internal interface IDatabase
{
    Task UpsertCategoriesStoredProcedureAsync(List<CategoryDto> categoryDtos);
}