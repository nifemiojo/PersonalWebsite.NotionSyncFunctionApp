using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties.Types;

internal class NotionCreatedTimePropertyType : NotionPagePropertyType
{
	[JsonPropertyName("created_time")]
	public string CreatedTime { get; set; }
}