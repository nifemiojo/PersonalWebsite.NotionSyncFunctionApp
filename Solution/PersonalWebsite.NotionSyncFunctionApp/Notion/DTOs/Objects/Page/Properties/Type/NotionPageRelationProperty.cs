using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

public class NotionPageRelationProperty : NotionPageProperty
{
    public List<NotionPageReference> Relation { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}

public class NotionPageReference
{
	public string Id { get; set; }
}