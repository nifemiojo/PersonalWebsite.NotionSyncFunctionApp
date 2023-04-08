using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

internal class EditedContent
{
	public List<Category> Categories { get; set; }

	public List<Playlist> Playlists { get; set; }

	public List<Post> Posts { get; set; }
}