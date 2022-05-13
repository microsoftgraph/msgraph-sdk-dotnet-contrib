using System;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class UpdateSiteScriptRequest
  {
    public Info updateInfo { get; set; }

    public UpdateSiteScriptRequest(string siteScriptId, SiteScriptMetadata siteScriptMetadata)
    {
      if (siteScriptId is null)
      {
        throw new ArgumentNullException(nameof(siteScriptId));
      }

      if (siteScriptMetadata == null)
      {
        throw new ArgumentNullException(nameof(siteScriptMetadata));
      }

      updateInfo = new Info
      {
        Id = siteScriptId,
        Title = siteScriptMetadata.Title,
        Description = siteScriptMetadata.Description,
        Version = siteScriptMetadata.Version,
        Content = siteScriptMetadata.Content
      };
    }


    public class Info : SiteScriptRequestInfo
    {
      [JsonPropertyName("Id")]
      public string Id { get; set; }
    }
  }
}
