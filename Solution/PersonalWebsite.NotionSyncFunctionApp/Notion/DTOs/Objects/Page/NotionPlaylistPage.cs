using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Collections;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

class NotionPlaylistPage : NotionPage
{
    public NotionPlaylistPagePropertiesCollection Properties { get; set; }

    public override BlogEntity MapToDomain()
    {
        return new Playlist
        {
            NotionPageId = Id,
            Name = Properties.Title.Title.Single().PlainText,
            Description = Properties.Description.RichText.Single().PlainText,
            CategoryNotionPageId = Properties.Category.Relation.Single().Id,
            Posts = Properties.Posts.Relation.Select(pageReference => pageReference.Id).ToList(),
            CreatedAt = Iso8601FormattedDateTime.CreateFromValid(CreatedTime),
            LastEditedTime = Iso8601FormattedDateTime.CreateFromValid(LastEditedTime)
        };
    }
}