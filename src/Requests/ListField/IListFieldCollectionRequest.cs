using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListFieldCollectionRequest: IBaseRequest
  {
    Task<IListFieldCollectionPage> GetAsync();
    Task<IListFieldCollectionPage> GetAsync(CancellationToken cancellationToken);
  }
}
