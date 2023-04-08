using System.Collections.Generic;
using PersonalWebsite.ContentSyncFunction.Domain;

namespace PersonalWebsite.ContentSyncFunction.Notion;

internal class EditedContent
{
	public List<Category> Categories { get; set; }

	public List<Playlist> Playlists { get; set; }

	public List<Post> Posts { get; set; }
}