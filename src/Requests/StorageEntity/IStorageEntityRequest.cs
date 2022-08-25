using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IStorageEntityRequest : IBaseRequest
  {
    Task<StorageEntity> GetAsync();
    Task<StorageEntity> GetAsync(CancellationToken cancellationToken);
  }
}
