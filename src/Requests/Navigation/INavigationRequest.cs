using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface INavigationRequest : IBaseRequest
  {
    Task<Navigation> GetAsync();
    Task<Navigation> GetAsync(CancellationToken cancellationToken);
  }
}
