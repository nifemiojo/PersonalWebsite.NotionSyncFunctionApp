using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties.Types;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;

internal class NotionPostProperties
{
	[JsonPropertyName("Title")]
	public NotionTitlePropertyType Title { get; set; }

	[JsonPropertyName("Description")]
	public NotionRichTextPropertyType Description { get; set; }

	[JsonPropertyName("Playlists")]
	public NotionRelationPropertyType Playlists { get; set; }

	[JsonPropertyName("Created Time")]
	public NotionCreatedTimePropertyType CreatedTime { get; set; }
}