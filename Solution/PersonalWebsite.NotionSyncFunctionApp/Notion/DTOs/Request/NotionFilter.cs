namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;

public class NotionFilter
{
    public string Property { get; set; }

    public NotionDateFilter Date { get; set; }
}