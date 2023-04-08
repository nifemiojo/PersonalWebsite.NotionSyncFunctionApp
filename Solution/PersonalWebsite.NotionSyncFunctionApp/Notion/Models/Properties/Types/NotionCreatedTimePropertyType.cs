using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties.Types;

internal class NotionCreatedTimePropertyType : NotionPagePropertyType
{
	[JsonPropertyName("created_time")]
	public string CreatedTime { get; set; }
}