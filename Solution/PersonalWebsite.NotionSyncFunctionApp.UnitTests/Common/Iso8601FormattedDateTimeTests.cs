using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.UnitTests.Common;

[TestFixture]
internal class Iso8601FormattedDateTimeTests
{
	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
	}

	[SetUp]
	public void PerTestSetUp()
	{

	}

	[Test]
	public void CreateFrom_ReturnsCorrectlyFormattedValue_WhenDateTimeKindIsUTC()
	{
		var dateTime = new DateTime(2023, 03, 02, 04, 27, 10, 555, DateTimeKind.Utc);

		var iso8601FormattedDateTime = Iso8601FormattedDateTime.CreateFrom(dateTime);

		iso8601FormattedDateTime.Value.Should().Be($"{dateTime:yyyy-MM-ddTHH:mm:ss.FFFZ}");
	}

	[Test]
	public void CreateFrom_ReturnsCorrectlyFormattedValue_WhenDateTimeKindIsLocal()
	{
		var dateTime = new DateTime(2023, 03, 02, 04, 27, 10, 555, DateTimeKind.Local);

		var iso8601FormattedDateTime = Iso8601FormattedDateTime.CreateFrom(dateTime);

		iso8601FormattedDateTime.Value.Should().Be($"{dateTime.ToUniversalTime():yyyy-MM-ddTHH:mm:ss.FFFZ}");
	}

	[TestCase("2023-03-02T04:27:00.555Z")]
	[TestCase("2023-03-02T15:27:00.555555Z")]
	[TestCase("2023-03-02T04:27:00.555Z")]
	[TestCase("2023-03-02T04:27:00.00055Z")]
	public void CreateFromValid_ReturnsValueWithSameTimestamp_WhenValidTimestamp(string timestamp)
	{
		var iso8601FormattedDateTime = Iso8601FormattedDateTime.CreateFromValid(timestamp);
		iso8601FormattedDateTime.Value.Should().Be(timestamp);
	}

	[Test]
	public void CreateFromValid_ReturnsValueWithEquivalentTimestampInUTC_WhenTimestampIncludesUTCOffset()
	{
		var actualTimestamp = "2023-03-02T04:27:00.465+03:00";
		var expectedTimestamp = "2023-03-02T01:27:00.465Z";

		var iso8601FormattedDateTime = Iso8601FormattedDateTime.CreateFromValid(actualTimestamp);

		iso8601FormattedDateTime.Value.Should().Be(expectedTimestamp);
	}

	[TestCase("Invalid")]
	[TestCase("2023-03-02T100:27:00.555Z")]
	[TestCase("2009-06-15T13:45:555Z")]
	[TestCase("2009-06-15T13:455:55Z")]
	[TestCase("2009-06-150T13:45:55Z")]
	[TestCase("2009-15-15T13:45:55Z")]
	public void CreateFromValid_ThrowsFormatException_WhenTimestampIsInvalid(string timestamp)
	{
		var createFromValid = () => Iso8601FormattedDateTime.CreateFromValid(timestamp);

		createFromValid.Should().Throw<FormatException>();
	}
}