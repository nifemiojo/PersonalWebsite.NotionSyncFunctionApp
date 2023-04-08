using System.Collections.Generic;

namespace PersonalWebsite.ContentSyncFunction.Notion.Response;

internal class NotionListResponse<TNotionObject>
{
    public string Object { get; set; }

    public List<TNotionObject> Results { get; set; }

    public string Type { get; set; }
}