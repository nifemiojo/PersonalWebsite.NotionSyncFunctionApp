using System.Collections.Generic;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;

internal class NotionListResponse<TNotionObject>
{
    public string Object { get; set; }

    public List<TNotionObject> Results { get; set; }

    public string Type { get; set; }
}