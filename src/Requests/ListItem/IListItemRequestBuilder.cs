using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListItemRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IListItemRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IListItemRequest Request(IEnumerable<Option> options);
  }
}
