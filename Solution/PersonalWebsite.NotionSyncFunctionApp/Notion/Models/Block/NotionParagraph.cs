using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;

namespace PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

internal class NotionParagraph
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	[JsonPropertyName("color")]
	public string Colour { get; set; }

	public List<NotionBlock> Children { get; set; }
}