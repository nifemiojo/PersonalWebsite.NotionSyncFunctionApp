using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;

namespace PersonalWebsite.ContentSyncFunction.Notion.Properties.PageProperty;

internal class NotionRelationPropertyType : NotionPagePropertyType
{
    public List<NotionPageReference> Relation { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}