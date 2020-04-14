using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteUserRequest : IBaseRequest
  {
    Task<SPUser> GetAsync();
    Task<SPUser> GetAsync(CancellationToken cancellationToken);
  }
}
