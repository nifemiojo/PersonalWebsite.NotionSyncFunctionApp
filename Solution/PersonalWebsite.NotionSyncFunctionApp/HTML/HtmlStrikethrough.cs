using System.Collections.Generic;
using PersonalWebsite.ContentSyncFunction.Common;

namespace PersonalWebsite.ContentSyncFunction.HTML;

class HtmlStrikethrough : HtmlElement
{
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = new List<HtmlElement>();
}