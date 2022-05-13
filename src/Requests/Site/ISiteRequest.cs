using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteRequest : IBaseRequest
  {
    Task<Site> GetAsync();
    Task<Site> GetAsync(CancellationToken cancellationToken);
  }
}
