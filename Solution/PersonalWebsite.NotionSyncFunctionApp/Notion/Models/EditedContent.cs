using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

internal class EditedContent
{
    public List<Category> Categories { get; set; }

    public List<Playlist> Playlists { get; set; }

    public List<Post> Posts { get; set; }
}