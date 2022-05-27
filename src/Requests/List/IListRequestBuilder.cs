using System;
using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListRequestBuilder : IBaseRequestBuilder
  {
    public IListItemCollectionRequestBuilder Items { get; }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IListRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IListRequest Request(IEnumerable<Option> options);
  }
}
