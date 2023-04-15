using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application;

public interface IContentManagementSystem
{
    Task<List<IDomainEntity>> GetUpdatedAsync<TDomainEntity>(LastSync lastSync) where TDomainEntity : IDomainEntity;
}