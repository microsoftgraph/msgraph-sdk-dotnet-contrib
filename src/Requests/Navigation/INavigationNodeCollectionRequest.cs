using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface INavigationNodeCollectionRequest : IBaseRequest
  {
    Task<ICollectionPage<NavigationNode>> GetAsync();
    Task<ICollectionPage<NavigationNode>> GetAsync(CancellationToken cancellationToken);

    Task<NavigationNode> AddAsync(NavigationNodeCreationInformation creationInformation);
    Task<NavigationNode> AddAsync(NavigationNodeCreationInformation creationInformation, CancellationToken cancellationToken);
  }
}
