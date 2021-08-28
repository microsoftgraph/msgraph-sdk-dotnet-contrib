using Microsoft.Graph;
using System.Collections.Generic;

namespace Graph.Community
{
  public interface ISiteGroupCollectionRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    ISiteGroupCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    ISiteGroupCollectionRequest Request(IEnumerable<Option> options);

    /// <summary>
    /// Gets an <see cref="ISiteUserRequestBuilder"/> for the specified User.
    /// </summary>
    /// <param name="id">The ID for the User.</param>
    /// <returns>The <see cref="ISiteUserRequestBuilder"/>.</returns>
    ISiteGroupRequestBuilder this[int id] { get; }
  }
}
