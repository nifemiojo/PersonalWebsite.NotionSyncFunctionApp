using System;

namespace PersonalWebsite.ContentSyncFunction.Notion.Exceptions;

internal class UnsuccessfulNotionRequest : Exception
{
    public UnsuccessfulNotionRequest(string? message) : base(message)
    { }
}