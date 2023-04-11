using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

class NotionPlaylistPageDto : NotionPageDto
{
	public NotionPlaylistProperties Properties { get; set; }
}