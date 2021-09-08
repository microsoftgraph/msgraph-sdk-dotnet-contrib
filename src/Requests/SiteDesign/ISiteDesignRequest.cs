using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteDesignRequest : IBaseRequest
  {
    Task<SiteDesignMetadata> GetAsync();
    Task<SiteDesignMetadata> GetAsync(CancellationToken cancellationToken);

    //_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteDesign

    Task<SiteDesignMetadata> UpdateAsync(SiteDesignMetadata updatedSiteDesignMetadata);
    Task<SiteDesignMetadata> UpdateAsync(SiteDesignMetadata updatedSiteDesignMetadata, CancellationToken cancellationToken);
  }
}
