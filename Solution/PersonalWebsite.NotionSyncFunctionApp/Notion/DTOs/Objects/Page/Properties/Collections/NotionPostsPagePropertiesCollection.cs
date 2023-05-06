using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Collections;

public class NotionPostsPagePropertiesCollection : NotionPagePropertiesCollection
{
    [JsonPropertyName("Description")]
    public NotionPageRichTextProperty Description { get; set; }

    [JsonPropertyName("Playlists")]
    public NotionPageRelationProperty Playlists { get; set; }

    [JsonPropertyName("Created Time")]
    public NotionPageCreatedTimeProperty CreatedTime { get; set; }
}