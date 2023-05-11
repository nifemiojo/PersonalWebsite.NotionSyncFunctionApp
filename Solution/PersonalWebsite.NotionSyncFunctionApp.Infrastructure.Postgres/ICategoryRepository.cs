namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

internal interface ICategoryRepository
{
	Task UpsertAsync<T>(List<CategoryEntity> categories) where T : Category;
}