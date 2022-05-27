using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface INavigationNodeCollectionRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    INavigationNodeCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    INavigationNodeCollectionRequest Request(IEnumerable<Option> options);
  }
}
