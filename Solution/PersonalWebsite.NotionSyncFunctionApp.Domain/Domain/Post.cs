namespace PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

public abstract class ContentBasedEntity  : BlogEntity
{
	public PostContent Content { get; set; }
}

public class Post : ContentBasedEntity
{
	public string NotionPageId { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public Iso8601FormattedDateTime CreatedAt { get; set; }

	public List<string> Playlists { get; set; }

	public Iso8601FormattedDateTime LastEditedTime { get; set; }

	public PostContent Content { get; set; }
}

public class PostContent
{
	public string Html { get; set; }
}