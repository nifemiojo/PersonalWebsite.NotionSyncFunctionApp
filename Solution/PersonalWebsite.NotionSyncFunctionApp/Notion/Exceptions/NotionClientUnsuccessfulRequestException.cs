using System;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Exceptions;

internal class NotionClientUnsuccessfulRequestException : Exception
{
	public NotionClientUnsuccessfulRequestException(string? message) : base(message)
	{ }

	public NotionClientUnsuccessfulRequestException(string? message, Exception? innerException) : base(message, innerException)
	{ }
}