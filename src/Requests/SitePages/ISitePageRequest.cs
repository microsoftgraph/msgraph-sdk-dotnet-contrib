using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISitePageRequest : IBaseRequest
  {
    Task<SitePageFileInfo> GetAsync();
    Task<SitePageFileInfo> GetAsync(CancellationToken cancellationToken);
  }
}
