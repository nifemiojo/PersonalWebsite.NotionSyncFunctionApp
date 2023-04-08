﻿using System.Text.Json.Serialization;
using PersonalWebsite.ContentSyncFunction.Notion.Properties.PageProperty;

namespace PersonalWebsite.ContentSyncFunction.Notion.Properties;

internal class NotionPlaylistProperties
{
    [JsonPropertyName("Title")]
    public NotionTitlePropertyType Title { get; set; }

    [JsonPropertyName("Description")]
    public NotionRichTextPropertyType Description { get; set; }

    [JsonPropertyName("Category")]
    public NotionRelationPropertyType Category { get; set; }
    
    [JsonPropertyName("Posts")]
    public NotionRelationPropertyType Posts { get; set; }

    [JsonPropertyName("Created Time")]
    public NotionCreatedTimePropertyType CreatedTime { get; set; }

}