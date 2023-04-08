using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties.Types;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties;

internal class NotionCategoryProperties
{
	[JsonPropertyName("Title")]
	public NotionTitlePropertyType Title { get; set; }
}