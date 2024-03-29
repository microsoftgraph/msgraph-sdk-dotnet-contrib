using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface INavigationNodeRequest : IBaseRequest
  {
    Task<NavigationNode> GetAsync();
    Task<NavigationNode> GetAsync(CancellationToken cancellationToken);

    Task<NavigationNode> UpdateAsync(NavigationNode navigationNode);
    Task<NavigationNode> UpdateAsync(NavigationNode navigationNode, CancellationToken cancellationToken);
  }
}
