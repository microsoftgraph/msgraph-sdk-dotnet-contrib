using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteDesignRequest : IBaseRequest
  {
    Task<ICollectionPage<SiteDesignMetadata>> GetAsync();
    Task<ICollectionPage<SiteDesignMetadata>> GetAsync(CancellationToken cancellationToken);

    Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest siteDesign);
    Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest siteDesign, CancellationToken cancellationToken);

    Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata);
    Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata, CancellationToken cancellationToken);

    //_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteDesign
  }
}
