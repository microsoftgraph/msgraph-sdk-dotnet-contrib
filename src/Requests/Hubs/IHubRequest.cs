using System.Threading.Tasks;
using System.Threading;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IHubRequest : IBaseRequest
  {
    Task<Hub> GetAsync();
    Task<Hub> GetAsync(CancellationToken cancellationToken);
  }
}
