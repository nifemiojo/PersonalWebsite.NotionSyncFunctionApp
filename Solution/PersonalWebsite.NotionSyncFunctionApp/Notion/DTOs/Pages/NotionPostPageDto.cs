using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

class NotionPostPageDto : NotionPageDto
{
	public NotionPostProperties Properties { get; set; }
}