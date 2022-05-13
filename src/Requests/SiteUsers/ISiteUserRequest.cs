using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteUserRequest : IBaseRequest
  {
    Task<User> GetAsync();
    Task<User> GetAsync(CancellationToken cancellationToken);
  }
}
