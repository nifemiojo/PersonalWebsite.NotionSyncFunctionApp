using System.Threading.Tasks;
using PersonalWebsite.ContentSyncFunction.Notion.Pages;

namespace PersonalWebsite.ContentSyncFunction.Notion;

internal interface INotionService
{
    // Think of this as an abstraction of the functionality of the Notion API
    // Task<NotionPage> RetrieveBlockChildren(string blockId);
    
    public Task<EditedContent> GetEditedNotionDatabaseContent();

}