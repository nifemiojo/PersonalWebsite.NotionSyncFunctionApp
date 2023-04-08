using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application;

public interface ILastSyncTimestampStorage
{
    Task<LastSync> Retrieve();
}