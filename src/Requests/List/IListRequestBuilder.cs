using System;
using Microsoft.Graph;

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
    IListRequestBuilder this[Guid id] { get; }

    /// <summary>
    /// Gets a <see cref="IListRequestBuilder"/> for the list with the specified title.
    /// </summary>
    /// <param name="title">The Title of the list</param>
    /// <returns></returns>
    IListRequestBuilder this[string title] { get; }

  }
}
