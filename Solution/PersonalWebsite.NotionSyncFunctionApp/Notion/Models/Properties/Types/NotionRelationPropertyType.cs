﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties.Types;

internal class NotionRelationPropertyType : NotionPagePropertyType
{
	public List<NotionPageReference> Relation { get; set; }

	[JsonPropertyName("has_more")]
	public bool HasMore { get; set; }
}