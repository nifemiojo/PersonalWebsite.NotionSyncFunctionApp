using System;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Exceptions;

internal class UnsuccessfulNotionRequest : Exception
{
	public UnsuccessfulNotionRequest(string? message) : base(message)
	{ }
}