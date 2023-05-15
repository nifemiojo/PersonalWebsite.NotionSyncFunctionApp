using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Collections;

internal class NotionPlaylistPagePropertiesCollection : NotionPagePropertiesCollection
{
    [JsonPropertyName("Description")]
    public NotionPageRichTextProperty Description { get; set; }

    [JsonPropertyName("CategoryNotionPageId")]
    public NotionPageRelationProperty Category { get; set; }

    [JsonPropertyName("Posts")]
    public NotionPageRelationProperty Posts { get; set; }

    [JsonPropertyName("Created Time")]
    public NotionPageCreatedTimeProperty CreatedTime { get; set; }
}