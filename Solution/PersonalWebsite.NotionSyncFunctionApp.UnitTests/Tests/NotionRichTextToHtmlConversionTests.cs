using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.UnitTests.Tests;

[TestFixture]
internal class NotionRichTextToHtmlConversionTests
{
	protected NotionRichTextToHtmlConversion _sut = null!;

	protected HtmlElement TopLevelElement { get; set; } = null!;
	protected const string PrimaryTestHref = "www.testone.com";
	protected const string SecondaryTestHref = "www.testtwo.com";

	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
	}

	[SetUp]
	public void PerTestSetUp()
	{
		TopLevelElement = new HtmlParagraph();
	}

	protected HtmlElement CallTestMethod(List<NotionRichText> richText)
	{
		_sut = new NotionRichTextToHtmlConversion(TopLevelElement, richText);
		return _sut.Convert();
	}

	protected NotionRichText CreateNotionRichText(string plainText)
	{
		return new NotionRichText
		{
			PlainText = plainText,
			Annotation = new NotionTextAnnotation(),
			Href = null
		};
	}

	protected NotionRichText CreateNotionRichTextWithSingleAnnotation(string plainText, InlineTextSemantic inlineTextSemantic, string href = PrimaryTestHref)
	{
		return new NotionRichText
		{
			PlainText = plainText,
			Annotation = CreateNotionTextAnnotation(new[] { inlineTextSemantic }),
			Href = inlineTextSemantic == InlineTextSemantic.Link ? href : null
		};
	}

	protected NotionRichText CreateNotionRichTextWithMultipleAnnotations(string plainText, IEnumerable<InlineTextSemantic> inlineTextSemantics, string href = PrimaryTestHref)
	{
		return new NotionRichText
		{
			PlainText = plainText,
			Annotation = CreateNotionTextAnnotation(inlineTextSemantics),
			Href = inlineTextSemantics.Contains(InlineTextSemantic.Link) ? href : null
		};
	}

	private static NotionTextAnnotation CreateNotionTextAnnotation(IEnumerable<InlineTextSemantic> textSemantics)
	{
		var notionTextAnnotation = new NotionTextAnnotation();

		foreach (var textSemantic in textSemantics)
		{
			switch (textSemantic)
			{
				case InlineTextSemantic.Bold:
				notionTextAnnotation.Bold = true;
				break;
				case InlineTextSemantic.Italic:
				notionTextAnnotation.Italic = true;
				break;
				case InlineTextSemantic.Strikethrough:
				notionTextAnnotation.Strikethrough = true;
				break;
				case InlineTextSemantic.Underline:
				notionTextAnnotation.Underline = true;
				break;
				case InlineTextSemantic.Code:
				notionTextAnnotation.Code = true;
				break;
			}
		}

		return notionTextAnnotation;
	}

	protected static void AssertElementHasCorrectTypeAndChildCount(HtmlElement actualHtmlElement,
		InlineTextSemantic expectedTextSemantic,
		string expectedHref = PrimaryTestHref,
		int childCount = 1)
	{
		AssertHtmlElementIsCorrectType(actualHtmlElement, expectedTextSemantic, expectedHref);

		actualHtmlElement.Children.Should().HaveCount(childCount);
	}

	protected static void AssertElementIsCorrectTypeAndHasSinglePlainTextChild(HtmlElement actualHtmlElement,
		InlineTextSemantic expectedTextSemantic,
		string expectedText,
		string expectedHref = PrimaryTestHref)
	{
		AssertElementHasCorrectTypeAndChildCount(actualHtmlElement, expectedTextSemantic, expectedHref);
		AssertPlainTextElementHasTextContent(actualHtmlElement.Children.Single(), expectedText);
	}

	protected static void AssertPlainTextElementHasTextContent(HtmlElement plainTextElement, string text)
	{
		plainTextElement.Should().BeOfType<HtmlPlainText>();
		(plainTextElement as HtmlPlainText)!.Content.Should().Be(text);
	}

	protected static void AssertHtmlElementIsCorrectType(HtmlElement actualHtmlElement, InlineTextSemantic expectedTextSemantic, string expectedHref = PrimaryTestHref)
	{
		actualHtmlElement.Should().BeOfType(InlineTextSemanticMapping.GetEquivalentHtmlElementType(expectedTextSemantic));

		if (expectedTextSemantic == InlineTextSemantic.Link)
		{
			var linkElement = actualHtmlElement as HtmlHyperlink;
			linkElement.Href.Should().Be(expectedHref);
		}
	}
}