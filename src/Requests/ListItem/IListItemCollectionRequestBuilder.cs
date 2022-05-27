using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListItemCollectionRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IListItemCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IListItemCollectionRequest Request(IEnumerable<Option> options);

    IListItemRequestBuilder this[int id] { get; }
  }
}
