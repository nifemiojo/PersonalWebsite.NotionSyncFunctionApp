using System;
using PersonalWebsite.NotionSyncFunctionApp.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

public class LastSync
{
	public Iso8601DateTime Timestamp { get; } = Iso8601DateTime.FromDateTime(DateTime.MinValue);

	protected LastSync()
	{
	}

    public LastSync(DateTime timestamp)
    {
        Timestamp = Iso8601DateTime.FromDateTime(timestamp);
    }

    public LastSync(Iso8601DateTime timestamp)
    {
        Timestamp = timestamp;
    }

    public LastSync(string timestamp)
    {
        Timestamp = Iso8601DateTime.FromString(timestamp);
    }
}