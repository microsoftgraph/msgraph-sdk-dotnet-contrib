using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteScriptRequest : IBaseRequest
  {
    Task<SiteScriptMetadata> GetAsync();
    Task<SiteScriptMetadata> GetAsync(CancellationToken cancellationToken);
  }
}
