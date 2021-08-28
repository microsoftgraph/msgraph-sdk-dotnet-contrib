using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteDesignRunRequest : IBaseRequest
  {
    Task<ISiteDesignRunCollectionPage> GetAsync();
    Task<ISiteDesignRunCollectionPage> GetAsync(CancellationToken cancellationToken);
  }
}
