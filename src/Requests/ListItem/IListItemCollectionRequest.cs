using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListItemCollectionRequest: IBaseRequest
  {
    Task<IListItemCollectionPage> GetAsync();
    Task<IListItemCollectionPage> GetAsync(CancellationToken cancellationToken);
  }
}
