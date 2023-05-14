using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Mapping;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

class CategoryRepository : ICategoryRepository
{
    private readonly IDatabase _database;

    public CategoryRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task UpsertAsync<T>(List<Category> categories) where T : Category
    {
        var categoryDtos = categories.Select(category => category.ToDto()).ToList();

        await _database.UpsertCategoriesStoredProcedureAsync(categoryDtos);
    }
}