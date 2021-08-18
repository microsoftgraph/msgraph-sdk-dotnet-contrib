using Microsoft.Graph;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteDesignRunRequestBuilder : BaseRequestBuilder, ISiteDesignRunRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteDesignRunRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }

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
