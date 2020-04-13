using Microsoft.Graph;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface IListRequest : IBaseRequest
  {
    Task<List> GetAsync();
    Task<List> GetAsync(CancellationToken cancellationToken);

    Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query);
    Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken);
  }
}
