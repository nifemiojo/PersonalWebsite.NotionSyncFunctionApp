using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties.Types;
using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties;

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