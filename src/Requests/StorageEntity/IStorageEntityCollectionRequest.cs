using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IStorageEntityCollectionRequest : IBaseRequest
  {
    Task<Dictionary<string, StorageEntity>> GetAsync();

    Task<Dictionary<string, StorageEntity>> GetAsync(CancellationToken cancellationToken);
  }
}
