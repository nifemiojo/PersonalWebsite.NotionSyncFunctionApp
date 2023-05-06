using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

public class NotionRichText
{
    public string Type { get; set; }

    public NotionText Text { get; set; }

    public NotionTextAnnotation Annotation { get; set; }

    [JsonPropertyName("plain_text")]
    public string PlainText { get; set; }

    public string? Href { get; set; }

    // TODO: Separate the behaviour from the data. This class should be a data class.
    public int TextSemanticsCount => GetTextSemantics().Count;

    public bool GetTextSemanticState(InlineTextSemantic inlineTextSemantic)
    {
        return inlineTextSemantic switch
        {
            InlineTextSemantic.Bold => Annotation.Bold,
            InlineTextSemantic.Italic => Annotation.Italic,
            InlineTextSemantic.Strikethrough => Annotation.Strikethrough,
            InlineTextSemantic.Underline => Annotation.Underline,
            InlineTextSemantic.Code => Annotation.Code,
            InlineTextSemantic.Link => Href != null,
            _ => throw new ArgumentOutOfRangeException(nameof(inlineTextSemantic), inlineTextSemantic, null)
        };
    }

    public bool TextSemanticsIncludes(InlineTextSemantic inlineTextSemantic, string? href)
    {
        var textSemanticState = GetTextSemanticState(inlineTextSemantic);

        if (textSemanticState == true && inlineTextSemantic == InlineTextSemantic.Link)
        {
            return Href == href;
        }

        return textSemanticState;
    }

    public List<InlineTextSemantic> GetTextSemantics()
    {
        var textSemantics = new List<InlineTextSemantic>();

        InlineTextSemanticMapping.GetAllInlineTextSemantics
            .ForEach(x =>
            {
                if (GetTextSemanticState(x))
                    textSemantics.Add(x);
            });

        return textSemantics;
    }

    public List<HtmlElement> GetHtmlElements()
    {
        var htmlElements = GetTextSemantics()
            .Select(InlineTextSemanticMapping.GetEquivalentHtmlElement).ToList();

        var linkElement = htmlElements.SingleOrDefault(x => x is HtmlHyperlink);
        if (linkElement != null)
        {
            ((HtmlHyperlink)linkElement).Href = Href!;
        }

        return htmlElements;
    }
}