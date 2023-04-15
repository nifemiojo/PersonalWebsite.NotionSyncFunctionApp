using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

internal class NotionPage : BaseNotionObject
{
	[JsonPropertyName("created_time")]
    public string CreatedTime { get; set; }

    [JsonPropertyName("last_edited_time")]
    public string LastEditedTime { get; set; }

	public virtual IDomainEntity Map()
	{
		throw new System.NotImplementedException();
	}
}