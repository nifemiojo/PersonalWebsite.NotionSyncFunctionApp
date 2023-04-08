using System.Text.Json.Serialization;
using PersonalWebsite.ContentSyncFunction.Notion.Properties.PageProperty;

namespace PersonalWebsite.ContentSyncFunction.Notion.Properties;

internal class NotionCategoryProperties
{
    [JsonPropertyName("Title")]
    public NotionTitlePropertyType Title { get; set; }
}