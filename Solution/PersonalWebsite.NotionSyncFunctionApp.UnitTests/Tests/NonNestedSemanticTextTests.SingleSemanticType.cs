using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.UnitTests.Tests;

/// <summary>
/// These tests cover cases where there is only a single active semantic type present in the entire input array of NotionRichTextObjects
/// e.g. [This is <b>an</b> example]
/// e.g. [<b>This</b> <b>is</b> an example]
/// </summary>
partial class NonNestedSemanticTextTests
{
	[TestCaseSource(nameof(SingleSemanticTypeCases))]
	public void Convert_ShouldReturnPlainTextNestedInElement_WhenSingleRichTextObjectWithSingleAnnotation(InlineTextSemantic inlineTextSemantic)
	{
		var text = "Hi, this is single annotation text.";
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation(text, inlineTextSemantic)
		};

		var topLevelElement = CallTestMethod(richText);

		// Assertions
		topLevelElement.Children.Should().HaveCount(1);

		var semanticElement = topLevelElement.Children.Single();
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, text);
	}

	[TestCaseSource(nameof(SingleSemanticTypeCases))]
	public void Convert_ShouldReturn_WhenSemanticInMiddle(InlineTextSemantic inlineTextSemantic)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This "),
			CreateNotionRichTextWithSingleAnnotation("is", inlineTextSemantic),
			CreateNotionRichText(" test"),
		};

		var result = CallTestMethod(richText);
		// Example: <p>This <b>is</b> test</p>

		result.Children.Should().HaveCount(3);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(0), "This ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(2), " test");

		var semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "is");
	}

	[TestCaseSource(nameof(SingleSemanticTypeCases))]
	public void Convert_ShouldReturn_WhenSemanticIsFirst(InlineTextSemantic inlineTextSemantic)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This ", inlineTextSemantic),
			CreateNotionRichText("is test"),
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This </b>is test</p>

		result.Children.Should().HaveCount(2);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(1), "is test");

		var semanticElement = result.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "This ");
	}

	[TestCaseSource(nameof(SingleSemanticTypeCases))]
	public void Convert_ShouldReturn_WhenSemanticTextIsLast(InlineTextSemantic inlineTextSemantic)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This is "),
			CreateNotionRichTextWithSingleAnnotation("test", inlineTextSemantic),
		};

		var result = CallTestMethod(richText);
		// Example: <p>This is <b>test</b></p>

		result.Children.Should().HaveCount(2);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(0), "This is ");

		var semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "test");
	}

	[TestCaseSource(nameof(SingleSemanticTypeCases))]
	public void Convert_ShouldReturn_WhenSemanticTextIsNeighbouringAtStart(InlineTextSemantic inlineTextSemantic)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This", inlineTextSemantic),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("is", inlineTextSemantic),
			CreateNotionRichText(" test"),
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This</b> <b>is<b> test</p>

		result.Children.Should().HaveCount(4);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(1), " ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(3), " test");

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "This");

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(2);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "is");
	}

	[TestCaseSource(nameof(SingleSemanticTypeCases))]
	public void Convert_ShouldReturn_WhenSemanticTextIsNeighbouringAtEnd(InlineTextSemantic inlineTextSemantic)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This "),
			CreateNotionRichTextWithSingleAnnotation("is", inlineTextSemantic),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("test", inlineTextSemantic),
		};

		var result = CallTestMethod(richText);
		// Example: <p>This <b>is</b> <b>test</b></p>

		result.Children.Should().HaveCount(4);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(0), "This ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(2), " ");

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "is");

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(3);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "test");
	}

	[TestCaseSource(nameof(SingleSemanticTypeCases))]
	public void Convert_ShouldReturn_WhenSemanticTextIsNeighbouringInMiddle(InlineTextSemantic inlineTextSemantic)
	{
		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This "),
			CreateNotionRichTextWithSingleAnnotation("is", inlineTextSemantic),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithSingleAnnotation("a", inlineTextSemantic),
			CreateNotionRichText(" test"),
		};

		var result = CallTestMethod(richText);
		// Example: <p>This <b>is</b> <b>a</b> test/p>

		result.Children.Should().HaveCount(5);

		AssertPlainTextElementHasTextContent(result.Children.ElementAt(0), "This ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(2), " ");
		AssertPlainTextElementHasTextContent(result.Children.ElementAt(4), " test");

		// Semantic Element 1
		var semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "is");

		// Semantic Element 2
		semanticElement = result.Children.ElementAt(3);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic, "a");
	}
}