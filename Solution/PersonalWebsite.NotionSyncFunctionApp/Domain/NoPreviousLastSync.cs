using System;
using PersonalWebsite.NotionSyncFunctionApp.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

public class NoPreviousLastSync : LastSync
{
	public NoPreviousLastSync()
	{
    }

    private NoPreviousLastSync(DateTime timestamp) : base(timestamp)
    {
    }

    private NoPreviousLastSync(Iso8601DateTime timestamp) : base(timestamp)
    {
    }

    private NoPreviousLastSync(string timestamp) : base(timestamp)
    {
    }
}