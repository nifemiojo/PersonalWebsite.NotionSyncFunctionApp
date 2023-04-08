using System;
using System.Collections.Generic;
using PersonalWebsite.ContentSyncFunction.HTML;

namespace PersonalWebsite.ContentSyncFunction.Common;

public class InlineTextSemanticMapping
{
	public static Type GetEquivalentHtmlElementType(InlineTextSemantic inlineTextSemantic)
	{
		return inlineTextSemantic switch
		{
			InlineTextSemantic.Bold => typeof(HtmlBold),
			InlineTextSemantic.Italic => typeof(HtmlItalic),
			InlineTextSemantic.Strikethrough => typeof(HtmlStrikethrough),
			InlineTextSemantic.Underline => typeof(HtmlUnderline),
			InlineTextSemantic.Code => typeof(HtmlCode),
			InlineTextSemantic.Link => typeof(HtmlHyperlink),
			_ => throw new ArgumentOutOfRangeException(nameof(inlineTextSemantic), inlineTextSemantic, null)
		};
	}
	
	public static HtmlElement GetEquivalentHtmlElement(InlineTextSemantic inlineTextSemantic)
	{
		return inlineTextSemantic switch
		{
			InlineTextSemantic.Bold => new HtmlBold(),
			InlineTextSemantic.Italic => new HtmlItalic(),
			InlineTextSemantic.Strikethrough => new HtmlStrikethrough(),
			InlineTextSemantic.Underline => new HtmlUnderline(),
			InlineTextSemantic.Code => new HtmlCode(),
			InlineTextSemantic.Link => new HtmlHyperlink(),
			_ => throw new ArgumentOutOfRangeException(nameof(inlineTextSemantic), inlineTextSemantic, null)
		};
	}
	
	public static InlineTextSemantic GetSemanticTextType(HtmlElement htmlElement)
	{
		return htmlElement switch
		{
			HtmlBold => InlineTextSemantic.Bold,
			HtmlItalic => InlineTextSemantic.Italic,
			HtmlStrikethrough => InlineTextSemantic.Strikethrough,
			HtmlUnderline => InlineTextSemantic.Underline,
			HtmlCode => InlineTextSemantic.Code,
			HtmlHyperlink => InlineTextSemantic.Link,
			_ => throw new ArgumentOutOfRangeException(nameof(htmlElement), htmlElement, null)
		};
	}

	public static List<InlineTextSemantic> GetAllInlineTextSemantics => new List<InlineTextSemantic>
	{
		InlineTextSemantic.Bold,
		InlineTextSemantic.Italic,
		InlineTextSemantic.Strikethrough,
		InlineTextSemantic.Underline,
		InlineTextSemantic.Code,
		InlineTextSemantic.Link,
	};
}