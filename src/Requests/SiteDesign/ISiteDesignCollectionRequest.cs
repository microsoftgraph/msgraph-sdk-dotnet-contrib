using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteDesignCollectionRequest : IBaseRequest
  {
    Task<ICollectionPage<SiteDesignMetadata>> GetAsync();
    Task<ICollectionPage<SiteDesignMetadata>> GetAsync(CancellationToken cancellationToken);

    Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest siteDesign);
    Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest siteDesign, CancellationToken cancellationToken);

    Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata);
    Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata, CancellationToken cancellationToken);
  }
}
