using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface INavigationNodeCollectionRequest : IBaseRequest
  {
    Task<INavigationNodeCollectionPage> GetAsync();
    Task<INavigationNodeCollectionPage> GetAsync(CancellationToken cancellationToken);

    Task<NavigationNode> AddAsync(NavigationNodeCreationInformation creationInformation);
    Task<NavigationNode> AddAsync(NavigationNodeCreationInformation creationInformation, CancellationToken cancellationToken);
  }
}
