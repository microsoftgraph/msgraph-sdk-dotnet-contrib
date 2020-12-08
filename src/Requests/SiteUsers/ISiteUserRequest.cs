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
    Task<User> GetAsync();
    Task<User> GetAsync(CancellationToken cancellationToken);
  }
}
