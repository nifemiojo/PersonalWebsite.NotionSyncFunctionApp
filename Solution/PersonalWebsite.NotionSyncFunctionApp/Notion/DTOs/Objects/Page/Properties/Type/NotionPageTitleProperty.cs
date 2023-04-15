﻿using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

public class NotionPageTitleProperty : NotionPageProperty
{
    public List<NotionRichText> Title { get; set; }
}