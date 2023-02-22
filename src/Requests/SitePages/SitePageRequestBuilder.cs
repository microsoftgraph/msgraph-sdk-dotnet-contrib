using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageRequestBuilder : BaseRequestBuilder, ISitePageRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SitePageRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public ISitePageRequest Request()
    {
      return this.Request(options);
    }

    public ISitePageRequest Request(IEnumerable<Option> options)
    {
      return new SitePageRequest(this.RequestUrl, this.Client, options);
    }

    public ISitePageVersionCollectionRequestBuilder Versions
    {
      get
      {
        return new Graph.Community.SitePageVersionCollectionRequestBuilder(this.AppendSegmentToRequestUrl("versions"), this.Client);
      }
    }
  }
}
