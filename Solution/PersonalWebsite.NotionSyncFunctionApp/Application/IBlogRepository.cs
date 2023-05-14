using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Application;

public interface IBlogRepository
{
	Task UpsertAsync<T>(List<BlogEntity> categories) where T : BlogEntity;
}