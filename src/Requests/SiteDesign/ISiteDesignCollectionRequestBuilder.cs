using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface ISiteDesignCollectionRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    ISiteDesignCollectionRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    ISiteDesignCollectionRequest Request(IEnumerable<Option> options);

    /// <summary>
    /// Gets a <see cref="ISiteDesignCollectionRequestBuilder"/> for the specified Site Design.
    /// </summary>
    /// <param name="id">The ID for the Site Design.</param>
    /// <returns>The <see cref="ISiteDesignCollectionRequestBuilder"/>.</returns>
    ISiteDesignRequestBuilder this[string id] { get; }
  }
}
