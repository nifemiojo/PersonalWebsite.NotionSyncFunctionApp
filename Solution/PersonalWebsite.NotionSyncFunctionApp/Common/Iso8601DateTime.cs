using System;
using System.Globalization;

namespace PersonalWebsite.NotionSyncFunctionApp.Common;

public class Iso8601DateTime
{
	private const string DateTimeStringFormat = "s";

	public DateTime Value { get; }

	private  Iso8601DateTime(DateTime dateTime)
	{
		Value = dateTime;
	}

	public static Iso8601DateTime FromDateTime(DateTime dateTime)
	{
		return new Iso8601DateTime(dateTime);
	}

	public static Iso8601DateTime FromString(string timestamp)
	{
		//return new Iso8601DateTime(DateTime.ParseExact(timestamp, DateTimeStringFormat, new CultureInfo("en-US")));
		return new Iso8601DateTime(DateTime.Parse(timestamp, CultureInfo.InvariantCulture));
	}

	public override string ToString()
	{
		return Value.ToString(DateTimeStringFormat);
	}
}