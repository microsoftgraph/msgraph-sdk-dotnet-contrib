using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteScriptRequest : IBaseRequest
  {
    Task<SiteScriptMetadata> GetAsync();
    Task<SiteScriptMetadata> GetAsync(CancellationToken cancellationToken);

    Task<SiteScriptMetadata> UpdateAsync(SiteScriptMetadata updatedSiteScriptMetadata);
    Task<SiteScriptMetadata> UpdateAsync(SiteScriptMetadata updatedSiteScriptMetadata, CancellationToken cancellationToken);

    Task DeleteAsync();
    Task DeleteAsync(CancellationToken cancellationToken);
  }
}
