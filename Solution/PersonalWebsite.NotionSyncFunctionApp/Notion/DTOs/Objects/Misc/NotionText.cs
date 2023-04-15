namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

public class NotionText
{
    public string Content { get; set; }

    public NotionTextLink Link { get; set; }
}

public class NotionTextLink
{
	public string Url { get; set; }
}