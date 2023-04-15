using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application;

public interface IContentManagementSystem
{
    Task<List<IDomainEntity>> GetUpdatedEntitiesAsync<TDomainEntity>(LastSync lastSync) where TDomainEntity : IDomainEntity;
}