using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class CreateSiteDesignRequest
  {
    public Info info { get; set; }

    public CreateSiteDesignRequest(SiteDesignMetadata siteDesignMetadata)
    {
      if (siteDesignMetadata == null)
      {
        throw new ArgumentNullException(nameof(siteDesignMetadata));
      }

      info = new Info
      {
        Title = siteDesignMetadata.Title,
        Description = siteDesignMetadata.Description,
        SiteScriptIds = siteDesignMetadata.SiteScriptIds,
        WebTemplate = siteDesignMetadata.WebTemplate,
        PreviewImageUrl = siteDesignMetadata.PreviewImageUrl,
        PreviewImageAltText = siteDesignMetadata.PreviewImageAltText,
        ThumbnailUrl = siteDesignMetadata.ThumbnailUrl,
      };

    }

    public class Info
    {
      [JsonPropertyName("Title")]
      public string Title { get; set; }

      [JsonPropertyName("Description")]
      public string Description { get; set; }

      [JsonPropertyName("SiteScriptIds")]
      public List<Guid> SiteScriptIds { get; set; }

      [JsonPropertyName("WebTemplate")]
      public string WebTemplate { get; set; }

      [JsonPropertyName("PreviewImageUrl")]
      public string PreviewImageUrl { get; set; }

      [JsonPropertyName("PreviewImageAltText")]
      public string PreviewImageAltText { get; set; }

      [JsonPropertyName("ThumbnailUrl")]
      public string ThumbnailUrl { get; set; }
    }

  }
}
