using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;

namespace PersonalWebsite.ContentSyncFunction.Notion.Properties.PageProperty;

internal class NotionRichTextPropertyType : NotionPagePropertyType
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }
}