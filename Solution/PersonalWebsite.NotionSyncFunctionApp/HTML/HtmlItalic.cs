﻿using PersonalWebsite.ContentSyncFunction.Common;
using System.Collections.Generic;

namespace PersonalWebsite.ContentSyncFunction.HTML;

class HtmlItalic : HtmlElement
{
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = new List<HtmlElement>();
}