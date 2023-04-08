using PersonalWebsite.ContentSyncFunction.Common;
using System.Collections.Generic;

namespace PersonalWebsite.ContentSyncFunction.HTML;

public class HtmlPlainText : HtmlElement
{
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = null;
	public string Content { get; set; }
}