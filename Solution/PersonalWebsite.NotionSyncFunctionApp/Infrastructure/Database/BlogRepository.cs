using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Database;

class BlogRepository : IBlogRepository
{
	public Task UpsertAsync<T>(List<BlogEntity> categories) where T : BlogEntity
	{
		if (typeof(T) == typeof(Category))
		{
			// Upsert categories
		}
		/*else if (typeof(T) == typeof(PostEntity))
		{
			// Upsert posts
		}*/
		else
		{
			throw new System.NotImplementedException();
		}
	}
}