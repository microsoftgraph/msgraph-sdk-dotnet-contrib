using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IHubCollectionRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IHubCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IHubCollectionRequest Request(IEnumerable<Option> options);

    /// <summary>
    /// Gets a <see cref="IHubRequestBuilder"/> for the specified Hub.
    /// </summary>
    /// <param name="id">The ID for the Hub.</param>
    /// <returns>The <see cref="IHubRequestBuilder"/>.</returns>
    IHubRequestBuilder this[string id] { get; }
  }
}
