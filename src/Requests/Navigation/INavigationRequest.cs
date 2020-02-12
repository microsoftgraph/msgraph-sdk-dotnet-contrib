using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface INavigationRequest : IBaseRequest
  {
    Task<Navigation> GetAsync();
    Task<Navigation> GetAsync(CancellationToken cancellationToken);
  }
}
