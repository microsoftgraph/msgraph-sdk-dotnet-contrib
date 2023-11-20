using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IHubCollectionRequest:IBaseRequest
  {
    Task<IHubCollectionPage> GetAsync();
    Task<IHubCollectionPage> GetAsync(CancellationToken cancellationToken);
  }
}
