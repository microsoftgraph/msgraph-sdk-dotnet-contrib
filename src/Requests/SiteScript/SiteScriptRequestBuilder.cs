using Microsoft.Graph;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteScriptRequestBuilder : BaseRequestBuilder, ISiteScriptRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteScriptRequestBuilder(
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
    public ISiteScriptRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public ISiteScriptRequest Request(IEnumerable<Option> options)
    {
      return new SiteScriptRequest(this.RequestUrl, this.Client, options);
    }
  }
}
