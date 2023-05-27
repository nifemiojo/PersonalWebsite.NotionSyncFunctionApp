using System.Globalization;

namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

public class Iso8601FormattedDateTime
{
    private const string DateTimeStringFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'FFFFFFFK";

    public string Value => RawDateTime.ToString(DateTimeStringFormat);

    private DateTime RawDateTime { get; }

    private Iso8601FormattedDateTime(DateTime dateTime)
    {
        RawDateTime = dateTime.ToUniversalTime();
    }

    public static Iso8601FormattedDateTime CreateFrom(DateTime dateTime)
    {
        return new Iso8601FormattedDateTime(dateTime);
    }

    public static Iso8601FormattedDateTime CreateFromValid(string timestamp)
    {
        try
        {
            var dateTime = DateTime.SpecifyKind(
                DateTime.ParseExact(timestamp, DateTimeStringFormat, CultureInfo.InvariantCulture),
                DateTimeKind.Utc);

            return new Iso8601FormattedDateTime(dateTime);
        }
        catch (FormatException originalException)
        {
            throw new FormatException($"Unable to parse timestamp: '{timestamp}' into a DateTime object with format: {DateTimeStringFormat}", originalException);
        }
    }

    public override string ToString()
    {
        return Value;
    }
}