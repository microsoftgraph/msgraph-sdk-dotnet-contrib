using Microsoft.Graph;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteRequestBuilder : BaseRequestBuilder, ISiteRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }

    public ISiteRequest Request()
    {
      return this.Request(this.options);
    }

    public ISiteRequest Request(IEnumerable<Option> options)
    {
      return new Graph.Community.SiteRequest(this.RequestUrl, this.Client, options);
    }
  }
}
