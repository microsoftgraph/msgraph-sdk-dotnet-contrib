using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class UpdateSiteDesignRequest
  {
    public Info updateInfo { get; set; }

    public UpdateSiteDesignRequest(string siteDesignId, SiteDesignMetadata siteDesignMetadata)
    {
      if (siteDesignId is null)
      {
        throw new ArgumentNullException(nameof(siteDesignId));
      }

      if (siteDesignMetadata == null)
      {
        throw new ArgumentNullException(nameof(siteDesignMetadata));
      }

      updateInfo = new Info
      {
        Id = siteDesignId,
        Title = siteDesignMetadata.Title,
        Description = siteDesignMetadata.Description,
        SiteScriptIds = siteDesignMetadata.SiteScriptIds,
        WebTemplate = siteDesignMetadata.WebTemplate,
        PreviewImageUrl = siteDesignMetadata.PreviewImageUrl,
        PreviewImageAltText = siteDesignMetadata.PreviewImageAltText
      };
    }


    public class Info: SiteDesignRequestInfo
    {
      [JsonPropertyName("Id")]
      public string Id { get; set; }
    }
  }
}
