namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;

internal class NotionFilter
{
    public string Property { get; set; }

    public NotionDateFilter Date { get; set; }
}