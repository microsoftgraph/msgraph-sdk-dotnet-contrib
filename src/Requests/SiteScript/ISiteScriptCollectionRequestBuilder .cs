using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface ISiteScriptCollectionRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    ISiteScriptCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    ISiteScriptCollectionRequest Request(IEnumerable<Option> options);

    /// <summary>
    /// Gets a <see cref="ISiteScriptRequestBuilder"/> for the specified Site Script.
    /// </summary>
    /// <param name="id">The ID for the Site Script.</param>
    /// <returns>The <see cref="ISiteScriptRequestBuilder"/>.</returns>
    ISiteScriptRequestBuilder this[string id] { get; }
  }
}
