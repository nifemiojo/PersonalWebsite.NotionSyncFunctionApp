using PersonalWebsite.ContentSyncFunction.Notion.Properties.PageProperty;
using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Properties;

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