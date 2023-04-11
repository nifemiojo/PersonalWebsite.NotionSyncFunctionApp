using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.UnitTests.Tests;

class NoSemanticTextTests : NotionRichTextToHtmlConversionTests
{
	[TestCase(typeof(HtmlParagraph))]
	public void Convert_ShouldReturnSameElementType_WhenPassedSupportedElementType(Type htmlElementType)
	{
		List<NotionRichText> richText = new List<NotionRichText>();
		var htmlElement = htmlElementType.InvokeMember("", BindingFlags.CreateInstance, null, null, null) as HtmlElement;

		var result = CallTestMethod(richText);

		result.Should().BeOfType(htmlElementType);
	}

	[Test]
	public void Convert_ShouldReturnSingleChildPlainTextElementWithCorrectText_WhenPassedSingleRichTextObjectWithNoAnnotations()
	{
		var plainText = "Hi, this is basic plain text.";
		List<NotionRichText> richText = new List<NotionRichText>
		{
			new NotionRichText
			{
				PlainText = plainText,
				Annotation = new NotionTextAnnotation
				{
					Bold = false,
					Italic = false,
					Strikethrough = false,
					Code = false
				},
				Href = null
			}
		};

		var result = CallTestMethod(richText);

		result.Children.Should().HaveCount(1);
		result.Children.Single().Should().BeOfType<HtmlPlainText>();
		(result.Children.Single() as HtmlPlainText).Content.Should().Be(plainText);
	}
}