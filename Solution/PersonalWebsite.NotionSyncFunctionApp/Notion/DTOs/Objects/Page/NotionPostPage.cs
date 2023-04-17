using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Collections;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

class NotionPostPage : NotionPage
{
    public NotionPostsPagePropertiesCollection Properties { get; set; }

    public override ContentBasedEntity MapToDomain()
    {
        return new Post
        {
            NotionPageId = Id,
            Name = Properties.Title.Title.Single().PlainText,
            Description = Properties.Description.RichText.Single().PlainText,
            Playlists = Properties.Playlists.Relation.Select(pageReference => pageReference.Id).ToList(),
            CreatedAt = Iso8601FormattedDateTime.CreateFromValid(CreatedTime),
            LastEditedTime = Iso8601FormattedDateTime.CreateFromValid(LastEditedTime),
        };
    }
}