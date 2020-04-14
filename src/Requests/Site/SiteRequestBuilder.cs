using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class SiteRequestBuilder : BaseRequestBuilder, ISiteRequestBuilder
  {
    private IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public SiteRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

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
