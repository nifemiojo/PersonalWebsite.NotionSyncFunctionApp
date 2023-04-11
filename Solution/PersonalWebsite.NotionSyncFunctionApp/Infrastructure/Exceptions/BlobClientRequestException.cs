using System;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Exceptions;

public class BlobClientRequestException : Exception
{
    public BlobClientRequestException(string message) : base(message)
    {
    }

    public BlobClientRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}