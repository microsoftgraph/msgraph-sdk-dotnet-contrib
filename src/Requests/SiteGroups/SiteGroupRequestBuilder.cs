using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SiteGroupRequestBuilder : BaseRequestBuilder, ISiteGroupRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteGroupRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }


    public ISiteGroupRequest Request()
    {
      return this.Request(options);
    }

    public ISiteGroupRequest Request(IEnumerable<Option> options)
    {
      return new SiteGroupRequest(this.RequestUrl, this.Client, options);
    }
  }
}
