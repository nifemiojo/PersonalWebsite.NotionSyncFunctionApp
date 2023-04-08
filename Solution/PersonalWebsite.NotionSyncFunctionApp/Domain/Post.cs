using System;
using System.Collections.Generic;

namespace PersonalWebsite.ContentSyncFunction.Domain;

internal class Post
{
	public string Id { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public DateTime CreatedAt { get; set; }

	public List<string> Playlists { get; set; }

	public DateTime LastEditedTime { get; set; }
}