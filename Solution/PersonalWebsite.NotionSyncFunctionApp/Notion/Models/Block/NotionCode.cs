using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

internal class NotionCode
{
	public List<NotionRichText> Caption { get; set; }

	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	public string Language { get; set; }
}