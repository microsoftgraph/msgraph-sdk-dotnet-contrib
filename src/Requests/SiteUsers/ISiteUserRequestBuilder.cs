using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteUserRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    ISiteUserRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    ISiteUserRequest Request(IEnumerable<Option> options);
  }
}
