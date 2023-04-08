using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Properties.PageProperty;

internal class NotionCreatedTimePropertyType : NotionPagePropertyType
{
	[JsonPropertyName("created_time")]
	public string CreatedTime { get; set; }
}