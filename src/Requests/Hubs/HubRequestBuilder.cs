using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class HubRequestBuilder : BaseRequestBuilder,IHubRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public HubRequestBuilder(
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
    public IHubRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public IHubRequest Request(IEnumerable<Option> options)
    {
      return new HubRequest(this.RequestUrl, this.Client, options);
    }
  }
}
