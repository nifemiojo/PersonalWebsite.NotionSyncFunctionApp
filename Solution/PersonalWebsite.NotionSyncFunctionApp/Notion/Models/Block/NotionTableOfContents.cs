using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

internal class NotionTableOfContents
{
	[JsonPropertyName("color")]
	public string Colour { get; set; }
}