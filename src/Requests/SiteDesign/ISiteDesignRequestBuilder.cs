using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteDesignRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    ISiteDesignRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    ISiteDesignRequest Request(IEnumerable<Option> options);
  }
}
