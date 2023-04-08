using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;

namespace PersonalWebsite.ContentSync.Tests.Tests;

/// <summary>
/// These tests cover cases where there are multiple active semantic types present in the entire input array of NotionRichTextObjects
/// e.g. This is <b>an</b> <u>example</u>
/// </summary>
partial class NonNestedSemanticTextTests
{
	[Test, Pairwise]
	public void Convert_WhenSemanticTextIsSeparatedByWhitespace(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This is", inlineTextSemantic1, PrimaryTestHref),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("test text", inlineTextSemantic2, SecondaryTestHref), // When testing links, hrefs need to be different in order to count as different sementic types
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This is</b> <u>test text</u>/p>

		result.Children.Should().HaveCount(3);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(1), " ");

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic1, "This is", PrimaryTestHref);

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(2);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic2, "test text", SecondaryTestHref);
	}
	
	[Test, Pairwise]
	public void Convert_WhenSemanticTextIsNotSeparatedByWhitespace(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		// The only case that is possible where semantics are same but not separated by whitespace is with links
		if (inlineTextSemantic1 == inlineTextSemantic2 && inlineTextSemantic1 is not InlineTextSemantic.Link)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This is ", inlineTextSemantic1, PrimaryTestHref),
			CreateNotionRichTextWithSingleAnnotation("test text", inlineTextSemantic2, SecondaryTestHref),
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This is </b><u>test text</u>/p>

		result.Children.Should().HaveCount(2);

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic1, "This is ", PrimaryTestHref);

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic2, "test text", SecondaryTestHref);
	}
	
	[Test, Pairwise]
	public void Convert_WhenPlainTextFollowedBySemanticTexts(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This is "),
			CreateNotionRichTextWithSingleAnnotation("test", inlineTextSemantic1, PrimaryTestHref),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("text", inlineTextSemantic2, SecondaryTestHref),
		};

		var result = CallTestMethod(richText);
		// Example: <p>This is <b>test</b> <u>text</u>/p>

		result.Children.Should().HaveCount(4);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(0), "This is ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(2), " ");

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic1, "test", PrimaryTestHref);

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(3);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic2, "text", SecondaryTestHref);
	}
	
	[Test, Pairwise]
	public void Convert_WhenSemanticTextsInMiddle(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This "),
			CreateNotionRichTextWithSingleAnnotation("is", inlineTextSemantic1, PrimaryTestHref),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("test", inlineTextSemantic2, SecondaryTestHref),
			CreateNotionRichText(" text"),
		};

		var result = CallTestMethod(richText);
		// Example: <p>This <b>is</b> <u>test</u> text/p>

		result.Children.Should().HaveCount(5);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(0), "This ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(2), " ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(4), " text");

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic1, "is", PrimaryTestHref);

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(3);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic2, "test", SecondaryTestHref);
	}
	
	[Test, Pairwise]
	public void Convert_WhenSemanticTextsAlternating(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This", inlineTextSemantic1, PrimaryTestHref),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("is", inlineTextSemantic2, SecondaryTestHref),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("test", inlineTextSemantic1, PrimaryTestHref),
			CreateNotionRichText(" "),  
			CreateNotionRichTextWithSingleAnnotation("text", inlineTextSemantic2, SecondaryTestHref),
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This</b> <u>is</u> <b>test</b> <u>text</u>/p>

		result.Children.Should().HaveCount(7);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(1), " ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(3), " ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(5), " ");

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic1, "This", PrimaryTestHref);

		// Semantic Element 3
		semanticElement = result.Children.ElementAt(4);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic1, "test", PrimaryTestHref);

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(2);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic2, "is", SecondaryTestHref);
		
		// Semantic Element 4
		semanticElement = result.Children.ElementAt(6);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic2, "text", SecondaryTestHref);
	}
}