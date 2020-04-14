using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface ISiteDesignRequestBuilder : IBaseRequestBuilder
  {
    ISiteDesignRequest Request();

    /// <summary>
    /// Gets a <see cref="ISiteDesignRequestBuilder"/> for the specified Site Design.
    /// </summary>
    /// <param name="id">The ID for the Site Design.</param>
    /// <returns>The <see cref="ISiteDesignRequestBuilder"/>.</returns>
    ISiteDesignRequestBuilder this[string id] { get; }
  }
}
