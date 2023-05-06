using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlHeadingElement : HtmlElement
{
    public HtmlHeadingElement(int i)
    {
        Tag = $"h{i}";
    }

    public override string? Tag { get; }
    public override List<HtmlElement>? Children { get; set; }
}