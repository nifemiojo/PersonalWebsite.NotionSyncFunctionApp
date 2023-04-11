using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties.Types;

internal class NotionRichTextPropertyType : NotionPagePropertyType
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }
}