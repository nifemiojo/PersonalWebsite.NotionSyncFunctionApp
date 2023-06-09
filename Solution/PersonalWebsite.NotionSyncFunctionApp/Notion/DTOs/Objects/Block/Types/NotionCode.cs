﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block.Types;

public class NotionCode
{
    public List<NotionRichText> Caption { get; set; }

    [JsonPropertyName("rich_text")]
    public List<NotionRichText> RichText { get; set; }

    public string Language { get; set; }
}