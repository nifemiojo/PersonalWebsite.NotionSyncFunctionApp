using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

internal interface IBlogEntityRepository
{
	Task UpsertAsync<T>(List<T> entitiesToUpsert) where T : BlogEntity;
}