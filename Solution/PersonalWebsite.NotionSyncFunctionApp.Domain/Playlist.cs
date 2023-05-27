namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

public class Playlist : BlogEntity
{
    public string NotionPageId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Iso8601FormattedDateTime CreatedAt { get; set; }

    public string CategoryNotionPageId { get; set; }

    public List<string> Posts { get; set; }

    public Iso8601FormattedDateTime LastEditedTime { get; set; }
}