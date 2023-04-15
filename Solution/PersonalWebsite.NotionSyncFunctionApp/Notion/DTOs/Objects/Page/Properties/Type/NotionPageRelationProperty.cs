using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

internal class NotionPageRelationProperty : NotionPageProperty
{
    public List<NotionPageReference> Relation { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}

internal class NotionPageReference
{
	public string Id { get; set; }
}