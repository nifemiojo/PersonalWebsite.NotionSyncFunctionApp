﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;

public class NotionHeading
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	[JsonPropertyName("is_toggleable")]
	public bool IsToggleable { get; set; }

	[JsonPropertyName("color")]
	public string Colour { get; set; }
}