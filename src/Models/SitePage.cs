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

    public DateTime? FirstPublishedDate { get; set; }
    
    [JsonPropertyName("Modified")]
    public new DateTimeOffset? LastModifiedDateTime { get; set; }
    
    public string FileName { get; set; }
    
    public string AbsoluteUrl { get; set; }
    
    public string Url { get; set; }
    
    public string UniqueId { get; set; }
  }
}
