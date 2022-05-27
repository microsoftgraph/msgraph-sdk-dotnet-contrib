using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListItemRequest:IBaseRequest
  {
    Task<ListItem> GetAsync();
    Task<ListItem> GetAsync(CancellationToken cancellationToken);
  }
}
