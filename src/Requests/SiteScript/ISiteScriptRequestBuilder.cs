using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface ISiteScriptRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    ISiteScriptRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    ISiteScriptRequest Request(IEnumerable<Option> options);
  }
}
