using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISitePageCollectionRequest : IBaseRequest
  {
    Task<ISitePageCollectionPage> GetAsync();
    Task<ISitePageCollectionPage> GetAsync(CancellationToken cancellationToken);

  }
}
