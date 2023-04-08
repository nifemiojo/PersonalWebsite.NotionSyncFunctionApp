using PersonalWebsite.ContentSyncFunction.Common;
using System.Collections.Generic;

namespace PersonalWebsite.ContentSyncFunction.HTML;

public class HtmlHyperlink : HtmlElement
{
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = new List<HtmlElement>();
	public string Href { get; set; }
}