using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.ContentSyncFunction.Common;
using PersonalWebsite.ContentSyncFunction.HTML;
using PersonalWebsite.ContentSyncFunction.Notion.Conversion;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;

namespace PersonalWebsite.ContentSync.Tests.Tests;

/// <summary>
/// These tests cover the cases where there is the s.
/// There are multiple semantic types in the nested  between all the input RTOs
/// e.g. <b><u>Here's an </u></b><s><b>example</b></s>
/// </summary>
partial class NestedSemanticTextTests
{
	[Test, Pairwise]
	public void Convert_WhenPartiallyNestedElementInMiddle(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2)
	{
		if (inlineTextSemantic1 == inlineTextSemantic2)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichText("This "),
			CreateNotionRichTextWithSingleAnnotation("is ", inlineTextSemantic1),
			CreateNotionRichTextWithMultipleAnnotations("test", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichTextWithSingleAnnotation(" text", inlineTextSemantic2)
		};

		var result = CallTestMethod(richText);
		// Example: <p>This <u>is </u><s><u>test</u> text</s</p>

		result.Children.Should().HaveCount(3);

		// Top Level Element 1
		var plainTextElement = result.Children.ElementAt(0);
		AssertPlainTextElementHasTextContent(plainTextElement, "This ");
		
		// Top Level Element 2
		var semanticElement = result.Children.ElementAt(1);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement, inlineTextSemantic1, "is ");

		// Top Level Element 3
		semanticElement = result.Children.ElementAt(2);
		AssertElementHasCorrectTypeAndChildCount(semanticElement, inlineTextSemantic2, childCount: 2);

		var semanticChild = semanticElement.Children.ElementAt(0);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild, inlineTextSemantic1, "test");

		semanticChild = semanticElement.Children.ElementAt(1);
		AssertPlainTextElementHasTextContent(semanticChild, " text");
	}

	[Test, Pairwise]
	public void Convert_WhenPartiallyNestedElementIsOneLevelDeep(
		[Values] InlineTextSemantic inlineTextSemantic1,
		[Values] InlineTextSemantic inlineTextSemantic2,
		[Values] InlineTextSemantic inlineTextSemantic3)
	{
		var textSemantics = new InlineTextSemantic[] { inlineTextSemantic1, inlineTextSemantic2, inlineTextSemantic3 };
		if (textSemantics.Distinct().Count() != 3)
			Assert.Pass();

		List<NotionRichText> richText = new List<NotionRichText>
		{
			CreateNotionRichTextWithMultipleAnnotations("This ", new[] { inlineTextSemantic1, inlineTextSemantic2 }),
			CreateNotionRichTextWithMultipleAnnotations("is test", new[] { inlineTextSemantic1, inlineTextSemantic2, inlineTextSemantic3 }),
			CreateNotionRichTextWithMultipleAnnotations(" text", new[] { inlineTextSemantic1, inlineTextSemantic3 }),
			CreateNotionRichTextWithSingleAnnotation(" long", inlineTextSemantic1),
		};

		var result = CallTestMethod(richText);
		// Example: <p><u><s>This </s><b><s>is test</s> text</b> long</u></p>

		result.Children.Should().HaveCount(1);

		// Top Level Element
		var semanticElement = result.Children.Single();
		AssertElementHasCorrectTypeAndChildCount(semanticElement, inlineTextSemantic1, childCount: 3);

		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticElement.Children.ElementAt(0), inlineTextSemantic2, "This ");

		var semanticChild = semanticElement.Children.ElementAt(1);
		AssertElementHasCorrectTypeAndChildCount(semanticChild, inlineTextSemantic3, childCount: 2);
		AssertElementIsCorrectTypeAndHasSinglePlainTextChild(semanticChild.Children.ElementAt(0), inlineTextSemantic2, "is test");
		AssertPlainTextElementHasTextContent(semanticChild.Children.ElementAt(1), " text");

		AssertPlainTextElementHasTextContent(semanticElement.Children.ElementAt(2), " long");

	}
}