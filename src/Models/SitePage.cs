using System;
using System.Text.Json.Serialization;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePage : BaseItem
  {
    public new int Id { get; set; }

    public string Title { get; set; }

    public SitePagePromotedState PromotedState { get; set; }

    [JsonPropertyName("FirstPublished")]
    public DateTimeOffset? FirstPublishedDateTime
    {
      get
      {
        //      "FirstPublished": "0001-01-01T08:00:00Z",
        if (firstPublished.HasValue && firstPublished.Value == SitePage.NULL_PUBLISHED_DATE)
        {
          return null;
        }
        return firstPublished;
      }
      set
      {
        firstPublished = value;
      }
    }
    private DateTimeOffset? firstPublished;
    private static DateTimeOffset NULL_PUBLISHED_DATE = new DateTimeOffset(0001, 01, 01, 08, 00, 00, TimeSpan.Zero);

    [JsonPropertyName("Modified")]
    public new DateTimeOffset? LastModifiedDateTime { get; set; }

    public string FileName { get; set; }

    public string AbsoluteUrl { get; set; }

    public string BannerImageUrl { get; set; }

    public string BannerThumbnailUrl { get; set; }

    public string Url { get; set; }

    public string UniqueId { get; set; }
  }
}
