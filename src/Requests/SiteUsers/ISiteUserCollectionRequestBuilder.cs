using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface ISiteUserCollectionRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    ISiteUserCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    ISiteUserCollectionRequest Request(IEnumerable<Option> options);

    /// <summary>
    /// Gets an <see cref="ISiteUserRequestBuilder"/> for the specified User.
    /// </summary>
    /// <param name="id">The ID for the User.</param>
    /// <returns>The <see cref="ISiteUserRequestBuilder"/>.</returns>
    ISiteUserRequestBuilder this[int id] { get; }

  }
}
