using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IStorageEntityCollectionRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IStorageEntityCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IStorageEntityCollectionRequest Request(IEnumerable<Option> options);

    /// <summary>
    /// Gets a <see cref="IListRequestBuilder"/> for the specified List.
    /// </summary>
    /// <param name="id">The ID of the list</param>
    /// <returns></returns>
    IStorageEntityRequestBuilder this[string key] { get; }
  }
}
