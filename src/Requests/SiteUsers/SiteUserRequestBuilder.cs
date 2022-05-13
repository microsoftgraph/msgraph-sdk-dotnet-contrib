using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SiteUserRequestBuilder : BaseRequestBuilder, ISiteUserRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteUserRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }


    public ISiteUserRequest Request()
    {
      return this.Request(options);
    }

    public ISiteUserRequest Request(IEnumerable<Option> options)
    {
      return new SiteUserRequest(this.RequestUrl, this.Client, options);
    }

  }
}
