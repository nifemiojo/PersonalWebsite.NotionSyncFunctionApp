using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

internal interface ICategoryRepository
{
    Task UpsertAsync<T>(List<Category> categories) where T : Category;
}