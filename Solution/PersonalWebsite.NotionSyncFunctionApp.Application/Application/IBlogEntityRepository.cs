using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application.Application;

public interface IBlogEntityRepository
{
    Task UpsertAsync<T>(List<BlogEntity> entitiesToUpsert) where T : BlogEntity;
}