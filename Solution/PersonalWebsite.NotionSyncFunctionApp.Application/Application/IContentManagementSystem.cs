using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application.Application;

public interface IContentManagementSystem
{
    Task<List<BlogEntity>> GetUpdatedBlogEntitiesAsync<TDomainEntity>(LastSync lastSync) where TDomainEntity : BlogEntity;
}