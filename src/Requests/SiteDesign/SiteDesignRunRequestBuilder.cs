using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class SiteDesignRunRequestBuilder : BaseRequestBuilder, ISiteDesignRunRequestBuilder
  {
    private IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public SiteDesignRunRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

/// <summary>
/// Builds the request
/// </summary>
/// <returns>The built request.</returns>
    public ISiteDesignRunRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public ISiteDesignRunRequest Request(IEnumerable<Option> options)
    {
      return new SiteDesignRunRequest(this.RequestUrl, this.Client, options);
    }

  }
}
