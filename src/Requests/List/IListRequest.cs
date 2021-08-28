using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface IListRequest : IBaseRequest
  {
    Task<List> GetAsync();
    Task<List> GetAsync(CancellationToken cancellationToken);

    Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query);
    Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken);
  }
}
