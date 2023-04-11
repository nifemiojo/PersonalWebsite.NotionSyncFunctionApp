using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

internal interface INotionService
{
	// Think of this as an abstraction of the functionality of the Notion API
	// Task<NotionPage> RetrieveBlockChildren(string blockId);

	public Task<EditedContent> GetEditedNotionDatabaseContent();

}