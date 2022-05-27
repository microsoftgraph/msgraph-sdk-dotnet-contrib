using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SitePageListItem : Graph.Community.ListItem
  {
    [JsonPropertyName("Created")]
    public new DateTimeOffset? CreatedDateTime { get; set; }

    [JsonPropertyName("Modified")]
    public new DateTimeOffset? LastModifiedDateTime { get; set; }

    public string ThumbnailUrl { get; set; }
    public DateTime? FirstPublishedDate { get; set; }

    [JsonConverter(typeof(SitePagePromotedStateConverterFactory))]
    public SitePagePromotedState PromotedState { get; set; }

    public UserInfo Author { get; set; }
    public UserInfo Editor { get; set; }
    public UserInfo CheckoutUser { get; set; }

    public List<int> ModernAudienceTargetUsers
    {
      get
      {
        var audienceRaw = this.AdditionalData["OData__ModernAudienceTargetUserFieldId"];
        if (audienceRaw == null ||
            audienceRaw is not JsonElement audienceJsonElement ||
            audienceJsonElement.ValueKind != JsonValueKind.Array)
        {
          return null;
        }
        var audienceTargetUsers = audienceJsonElement.Deserialize<List<int>>();
        return audienceTargetUsers;
      }
    }
  }
}
