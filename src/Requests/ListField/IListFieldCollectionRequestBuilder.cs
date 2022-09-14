using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListFieldCollectionRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IListFieldCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IListFieldCollectionRequest Request(IEnumerable<Option> options);

    IListFieldRequestBuilder this[string id] { get; }

  }
}
