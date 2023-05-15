using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

public abstract class NotionPage : BaseNotionObject
{
	[JsonPropertyName("created_time")]
    public string CreatedTime { get; set; }

    [JsonPropertyName("last_edited_time")]
    public string LastEditedTime { get; set; }

    public abstract BlogEntity MapToDomain();
}