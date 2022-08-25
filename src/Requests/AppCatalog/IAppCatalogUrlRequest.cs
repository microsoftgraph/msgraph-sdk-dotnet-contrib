using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IAppCatalogUrlRequest:IBaseRequest
  {
    Task<string> GetAsync();
    Task<string> GetAsync(CancellationToken cancellationToken);
  }
}
