using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteDesignCollectionRequest : IBaseRequest
  {
    Task<ISiteDesignCollectionPage> GetAsync();
    Task<ISiteDesignCollectionPage> GetAsync(CancellationToken cancellationToken);

    Task<IApplySiteDesignActionOutcomeCollectionPage> ApplyAsync(ApplySiteDesignRequest siteDesign);
    Task<IApplySiteDesignActionOutcomeCollectionPage> ApplyAsync(ApplySiteDesignRequest siteDesign, CancellationToken cancellationToken);

    Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata);
    Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata, CancellationToken cancellationToken);

    //_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteDesign
  }
}
