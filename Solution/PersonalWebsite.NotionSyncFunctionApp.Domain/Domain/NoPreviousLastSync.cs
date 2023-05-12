namespace PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

public class NoPreviousLastSync : LastSync
{
	public NoPreviousLastSync()
	{
    }

    private NoPreviousLastSync(DateTime timestamp) : base(timestamp)
    {
    }

    private NoPreviousLastSync(Iso8601FormattedDateTime timestamp) : base(timestamp)
    {
    }

    private NoPreviousLastSync(string timestamp) : base(timestamp)
    {
    }
}