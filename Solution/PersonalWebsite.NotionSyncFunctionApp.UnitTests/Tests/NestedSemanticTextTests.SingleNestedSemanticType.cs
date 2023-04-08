using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;

namespace PersonalWebsite.ContentSync.Tests.Tests;

/// <summary>
/// These tests cover where there is at least one RTO that has at most two text semantics.
/// There at at most two types of text semantic present within all RTOs
/// e.g. <b><u>This</u> is test text</b>
/// </summary>
partial class NestedSemanticTextTests
{
	[Test, Pairwise]
	public void Convert_WhenFullyNestedSemanticsIsAtStart(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		if (inlineTextSemantic1 == inlineTextSemantic2)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithMultipleAnnotations("This", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichTextWithSingleAnnotation(" is test text", inlineTextSemantic1)
		};

		var result = CallTestMethod(richText);
		// Example: <p><b><u>This<u> is test text</b></p>

		result.Children.Should().HaveCount(1);

		// Top Level Semantic Element
		var semanticElement = result.Children.ElementAt(0);
		AssertElementHasCorrectTypeAndChildCount(semanticElement, inlineTextSemantic1, childCount: 2);

		// Semantic Child 1
		var semanticChild = semanticElement.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic2, "This",
			PrimaryTestHref);

		// Semantic Child 2
		semanticChild = semanticElement.Children.ElementAt(1);
		AssertPlainTextElementHasTextContent(semanticChild, " is test text");
	}

	[Test, Pairwise]
	public void Convert_WhenFullyNestedSemanticsIsAtEnd(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		if (inlineTextSemantic1 == inlineTextSemantic2)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This is test ", inlineTextSemantic1),
			CreateNotionRichTextWithMultipleAnnotations("text", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This is test <u>text</u></b></p>

		result.Children.Should().HaveCount(1);

		// Top Level Semantic Element
		var semanticElement = result.Children.ElementAt(0);
		AssertElementHasCorrectTypeAndChildCount(semanticElement, inlineTextSemantic1, childCount: 2);

		// Semantic Child 1
		var semanticChild = semanticElement.Children.ElementAt(0);
		AssertPlainTextElementHasTextContent(semanticChild, "This is test ");

		// Semantic Child 2
		semanticChild = semanticElement.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic2, "text");
	}

	[Test, Pairwise]
	public void Convert_WhenFullyNestedSemanticsIsInMiddle(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		if (inlineTextSemantic1 == inlineTextSemantic2)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This ", inlineTextSemantic1),
			CreateNotionRichTextWithMultipleAnnotations("is test", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichTextWithSingleAnnotation(" text", inlineTextSemantic1),
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This <u>is test</u> text</b></p>

		result.Children.Should().HaveCount(1);

		// Top Level Semantic Element
		var semanticElement = result.Children.ElementAt(0);
		AssertElementHasCorrectTypeAndChildCount(semanticElement, inlineTextSemantic1, childCount: 3);

		// Semantic Child 1
		var semanticChild = semanticElement.Children.ElementAt(0);
		AssertPlainTextElementHasTextContent(semanticChild, "This ");

		// Semantic Child 2
		semanticChild = semanticElement.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic2, "is test",
			PrimaryTestHref);

		// Semantic Child 3
		semanticChild = semanticElement.Children.ElementAt(2);
		AssertPlainTextElementHasTextContent(semanticChild, " text");
	}

	[Test, Pairwise]
	public void Convert_WhenFullyNestedSemanticsIsSeparatedInMiddle(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		if (inlineTextSemantic1 == inlineTextSemantic2)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithSingleAnnotation("This ", inlineTextSemantic1),
			CreateNotionRichTextWithMultipleAnnotations("is", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichTextWithSingleAnnotation(" ", inlineTextSemantic1),
			CreateNotionRichTextWithMultipleAnnotations("test", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichTextWithSingleAnnotation(" text", inlineTextSemantic1),
		};

		var result = CallTestMethod(richText);
		// Example: <p><b>This <u>is</u> <u>test</u> text</b></p>

		result.Children.Should().HaveCount(1);

		// Top Level Semantic Element
		var semanticElement = result.Children.ElementAt(0);
		AssertElementHasCorrectTypeAndChildCount(semanticElement, inlineTextSemantic1, childCount: 5);

		// Semantic Child 1
		var semanticChild = semanticElement.Children.ElementAt(0);
		AssertPlainTextElementHasTextContent(semanticChild, "This ");

		// Semantic Child 2
		semanticChild = semanticElement.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic2, "is", PrimaryTestHref);

		// Semantic Child 3
		semanticChild = semanticElement.Children.ElementAt(2);
		AssertPlainTextElementHasTextContent(semanticChild, " ");

		// Semantic Child 4
		semanticChild = semanticElement.Children.ElementAt(3);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic2, "test",
			PrimaryTestHref);

		// Semantic Child 5
		semanticChild = semanticElement.Children.ElementAt(4);
		AssertPlainTextElementHasTextContent(semanticChild, " text");
	}

	[Test, Pairwise]
	public void Convert_WhenFullyNestedSemanticsIsBetweenPlainText(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		if (inlineTextSemantic1 == inlineTextSemantic2)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This "),
			CreateNotionRichTextWithMultipleAnnotations("is", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichTextWithSingleAnnotation(" ", inlineTextSemantic1),
			CreateNotionRichTextWithMultipleAnnotations("test", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichText(" text")
		};

		var result = CallTestMethod(richText);
		// Example: <p>This <u><s>is</s> <s>test</s></u> text</p>

		result.Children.Should().HaveCount(3);

		// Top Level Element 1
		var plainTextElement = result.Children.ElementAt(0);
		AssertPlainTextElementHasTextContent(plainTextElement, "This ");

		// Top Level Element 2
		var semanticElement = result.Children.ElementAt(1);
		AssertElementHasCorrectTypeAndChildCount(semanticElement, inlineTextSemantic1, childCount: 3);

		// Top Level Element 3
		plainTextElement = result.Children.ElementAt(2);
		AssertPlainTextElementHasTextContent(plainTextElement, " text");

		// Semantic Child 1
		var semanticChild = semanticElement.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic2, "is");

		// Semantic Child 2
		semanticChild = semanticElement.Children.ElementAt(1);
		AssertPlainTextElementHasTextContent(semanticChild, " ");

		// Semantic Child 3
		semanticChild = semanticElement.Children.ElementAt(2);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic2, "test");
	}

	[Test, Pairwise]
	public void Convert_WhenFullyNestedSemanticsAreSeparatedByPlainText(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		if (inlineTextSemantic1 == inlineTextSemantic2)
			Assert.Pass();

		var textSemantics = new[] { inlineTextSemantic1, inlineTextSemantic2 };

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithMultipleAnnotations("This is", textSemantics),
			CreateNotionRichText(" "),
			CreateNotionRichTextWithMultipleAnnotations("test text", textSemantics),
		};

		var result = CallTestMethod(richText);
		// Example: <p><u><s>This is </s></u> <u><s>test text</u></s></p>

		result.Children.Should().HaveCount(3);

		// Top Level Element 1
		var plainTextElement = result.Children.ElementAt(1);
		AssertPlainTextElementHasTextContent(plainTextElement, " ");

		// Top Level Element 2
		var semanticElement = result.Children.ElementAt(0);
		semanticElement.Children.Should().HaveCount(1);

		// Semantic Child 1
		var semanticChild = semanticElement.Children.ElementAt(0);
		semanticElement.Children.Should().HaveCount(1);
		AssertPlainTextElementHasTextContent(semanticChild.Children.Single(), "This is");

		textSemantics
			.Select(InlineTextSemanticMapping.GetEquivalentHtmlElementType)
			.Should().BeEquivalentTo(new Type[] { semanticElement.GetType(), semanticChild.GetType() });


		// Top Level Element 3
		semanticElement = result.Children.ElementAt(2);
		semanticElement.Children.Should().HaveCount(1);

		// Semantic Child 2
		semanticChild = semanticElement.Children.ElementAt(0);
		semanticElement.Children.Should().HaveCount(1);
		AssertPlainTextElementHasTextContent(semanticChild.Children.Single(), "test text");

		textSemantics
			.Select(InlineTextSemanticMapping.GetEquivalentHtmlElementType)
			.Should().BeEquivalentTo(new Type[] { semanticElement.GetType(), semanticChild.GetType() });
	}
}