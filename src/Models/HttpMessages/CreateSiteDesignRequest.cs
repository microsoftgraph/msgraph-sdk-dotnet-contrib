using System;
using System.Collections.Generic;
using System.Text;

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
        PreviewImageAltText = siteDesignMetadata.PreviewImageAltText
      };

    }

    public class Info
    {
      public string Title { get; set; }
      public string Description { get; set; }
      public List<Guid> SiteScriptIds { get; set; }
      public string WebTemplate { get; set; }
      public string PreviewImageUrl { get; set; }
      public string PreviewImageAltText { get; set; }
    }

  }
}
