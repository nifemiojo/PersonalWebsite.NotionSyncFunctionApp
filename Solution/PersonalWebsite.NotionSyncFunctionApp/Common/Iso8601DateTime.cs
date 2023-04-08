using System;
using System.Globalization;

namespace PersonalWebsite.NotionSyncFunctionApp.Common;

public class Iso8601DateTime
{
	private const string DateTimeStringFormat = "s";

	private DateTime DateTime { get; }

	public override string ToString()
	{
		return DateTime.ToString(DateTimeStringFormat);
	}

	public Iso8601DateTime(DateTime dateTime)
	{
		DateTime = dateTime;
	}

	// FromDateTime
	public static Iso8601DateTime FromDateTime(DateTime dateTime)
	{
		return new Iso8601DateTime(dateTime);
	}

	public static Iso8601DateTime FromString(string timestamp)
	{
		//return new Iso8601DateTime(DateTime.ParseExact(timestamp, DateTimeStringFormat, new CultureInfo("en-US")));
		return new Iso8601DateTime(DateTime.Parse(timestamp, new CultureInfo("en-US")));
	}
}