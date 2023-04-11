using System;
using PersonalWebsite.NotionSyncFunctionApp.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

public class LastSync
{
	public Iso8601FormattedDateTime Timestamp { get; } = Iso8601FormattedDateTime.CreateFrom(DateTime.MinValue);

	protected LastSync()
	{
	}

    public LastSync(DateTime timestamp)
    {
        Timestamp = Iso8601FormattedDateTime.CreateFrom(timestamp);
    }

    public LastSync(Iso8601FormattedDateTime timestamp)
    {
        Timestamp = timestamp;
    }

    public LastSync(string timestamp)
    {
        Timestamp = Iso8601FormattedDateTime.CreateFromValid(timestamp);
    }
}