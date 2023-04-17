using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block.Types;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;

public class NotionBlock : BaseNotionObject
{
    [JsonPropertyName("has_children")]
    public bool HasChildren { get; set; }

    public string Type { get; set; }

    // Block Types
    [JsonPropertyName("bulleted_list_item")]
    public NotionBulletedListItem BulletedListItem { get; set; }

    public NotionCode Code { get; set; }

    [JsonPropertyName("heading_1")]
    public NotionHeading HeadingOne { get; set; }

    [JsonPropertyName("heading_2")]
    public NotionHeading HeadingTwo { get; set; }

    [JsonPropertyName("heading_3")]
    public NotionHeading HeadingThree { get; set; }

    public NotionFile Image { get; set; }

    [JsonPropertyName("numbered_list_item")]
    public NotionNumberedListItem NumberedListItem { get; set; }

    public NotionParagraph Paragraph { get; set; }

    public NotionQuote Quote { get; set; }

    public List<NotionBlock> ChildBlocks { get; set; }
}