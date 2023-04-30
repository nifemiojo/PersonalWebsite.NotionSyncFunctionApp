using System.Collections.Generic;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

internal class HtmlOrderedListElement : HtmlElement
{
    public override string? Tag { get; }
    public override List<HtmlElement>? Children { get; set; }
}