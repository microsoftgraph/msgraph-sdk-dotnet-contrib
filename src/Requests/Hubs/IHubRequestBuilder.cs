using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IHubRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IHubRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IHubRequest Request(IEnumerable<Option> options);
  }
}
