using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteDesignRunRequest : IBaseRequest
  {
    Task<ISiteDesignRunCollectionPage> GetAsync();
    Task<ISiteDesignRunCollectionPage> GetAsync(CancellationToken cancellationToken);
  }
}
