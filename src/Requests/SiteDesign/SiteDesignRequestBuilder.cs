using Microsoft.Graph;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteDesignRequestBuilder : BaseRequestBuilder, ISiteDesignRequestBuilder
  {
    private IEnumerable<Option> options;

    public SiteDesignRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    public ISiteDesignRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public ISiteDesignRequest Request(IEnumerable<Option> options)
    {
      return new SiteDesignRequest(this.RequestUrl, this.Client, options);
    }
  }
}
