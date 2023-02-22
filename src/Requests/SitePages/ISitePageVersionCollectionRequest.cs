using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISitePageVersionCollectionRequest : IBaseRequest
  {
    Task<ISitePageVersionCollectionPage> GetAsync();
    Task<ISitePageVersionCollectionPage> GetAsync(CancellationToken cancellationToken);
  }
}
