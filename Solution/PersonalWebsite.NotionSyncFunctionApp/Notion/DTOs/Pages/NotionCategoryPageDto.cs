using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

class NotionCategoryPageDto : NotionPageDto
{
	public NotionCategoryProperties Properties { get; set; }
}