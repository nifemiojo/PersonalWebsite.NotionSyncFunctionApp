using System;
using System.Collections.Generic;

namespace PersonalWebsite.ContentSyncFunction.Domain;

internal class Playlist
{
	public string Id { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public DateTime CreatedAt { get; set; }
	 
	public string Category { get; set; }

	public List<string> Posts { get; set; }

	public DateTime LastEditedTime { get; set; }
}