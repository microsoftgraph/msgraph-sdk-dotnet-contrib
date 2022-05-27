using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListCollectionRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IListCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IListCollectionRequest Request(IEnumerable<Option> options);

    /// <summary>
    /// Gets a <see cref="IListRequestBuilder"/> for the specified List.
    /// </summary>
    /// <param name="id">The ID of the list</param>
    /// <returns></returns>
    IListRequestBuilder this[Guid id] { get; }

    /// <summary>
    /// Gets a <see cref="IListRequestBuilder"/> for the list with the specified title.
    /// </summary>
    /// <param name="title">The Title of the list</param>
    /// <returns></returns>
    IListRequestBuilder this[string title] { get; }
  }
}
