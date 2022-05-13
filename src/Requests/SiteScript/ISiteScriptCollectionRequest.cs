using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteScriptCollectionRequest : IBaseRequest
  {
    Task<ISiteScriptCollectionPage> GetAsync();
    Task<ISiteScriptCollectionPage> GetAsync(CancellationToken cancellationToken);

    Task<SiteScriptMetadata> CreateAsync(SiteScriptMetadata siteScriptMetadata);
    Task<SiteScriptMetadata> CreateAsync(SiteScriptMetadata siteScriptMetadata, CancellationToken cancellationToken);
  }
}
