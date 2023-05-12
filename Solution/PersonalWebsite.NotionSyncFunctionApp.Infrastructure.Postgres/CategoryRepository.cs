using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.MappingExtensions;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

class CategoryRepository : ICategoryRepository
{
	private readonly IDatabase _database;

	public CategoryRepository(IDatabase database)
	{
		_database = database;
	}

	public async Task UpsertAsync<T>(List<Category> categories) where T : Category
	{
		// Map domain category to Dto category
		var categoryDtos = categories.Select(category => category.ToDto()).ToList();

		// Upsert Dto category
		await _database.NonQueryOperationAsync<T>("placeholder", new { });
	}
}