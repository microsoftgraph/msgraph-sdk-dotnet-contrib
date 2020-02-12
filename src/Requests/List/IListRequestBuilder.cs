using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface IListRequestBuilder : IBaseRequestBuilder
  {
    IListRequest Request();

    /// <summary>
    /// Gets a <see cref="IListRequestBuilder"/> for the specified List.
    /// </summary>
    /// <param name="id">The ID of the list</param>
    /// <returns></returns>
#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
    IListRequestBuilder this[Guid id] { get; }
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers

    /// <summary>
    /// Gets a <see cref="IListRequestBuilder"/> for the list with the specified title.
    /// </summary>
    /// <param name="title">The Title of the list</param>
    /// <returns></returns>
    IListRequestBuilder this[string title] { get; }

  }
}
