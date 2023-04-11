using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties.Types;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;

internal class NotionCategoryProperties
{
	[JsonPropertyName("Title")]
	public NotionTitlePropertyType Title { get; set; }
}