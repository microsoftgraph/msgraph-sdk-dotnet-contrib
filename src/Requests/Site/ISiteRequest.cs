using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteRequest : IBaseRequest
  {
    Task<Site> GetAsync();
    Task<Site> GetAsync(CancellationToken cancellationToken);
  }
}
