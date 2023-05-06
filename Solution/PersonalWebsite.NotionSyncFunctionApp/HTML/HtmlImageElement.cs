using System;
using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlImageElement : HtmlElement
{
    private readonly Uri _src;

    public HtmlImageElement(Uri src)
    {
        _src = src;
    }

    public override string? Tag { get; }
    public override List<HtmlElement>? Children { get; set; }
}