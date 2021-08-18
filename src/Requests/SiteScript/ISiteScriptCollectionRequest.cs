using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

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
