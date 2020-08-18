using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface ISiteDesignRunRequest:IBaseRequest
  {
    Task<ICollectionPage<SiteDesignRun>> GetAsync();
    Task<ICollectionPage<SiteDesignRun>> GetAsync(CancellationToken cancellationToken);
  }
}
