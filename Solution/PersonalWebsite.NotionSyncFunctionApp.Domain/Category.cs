namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

public class Category : BlogEntity
{
    public string NotionPageId { get; set; }

    public string Name { get; set; }

    public Iso8601FormattedDateTime CreatedAt { get; set; }

    public Iso8601FormattedDateTime LastEditedTime { get; set; }
}