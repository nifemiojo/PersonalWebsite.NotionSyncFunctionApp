using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application;

public interface IContentManagementSystem
{
    Task<List<IDomainEntity>> GetUpdatedEntitiesAsync<TDomainEntity>(UpdatedEntitiesQuery query) where TDomainEntity : IDomainEntity;
}