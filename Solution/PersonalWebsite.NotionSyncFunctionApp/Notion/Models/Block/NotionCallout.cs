using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

internal class NotionCallout
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	public NotionIcon Icon { get; set; }

	[JsonPropertyName("color")]
	public string Colour { get; set; }
}