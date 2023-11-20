using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class HubCollectionRequestBuilder : BaseRequestBuilder, IHubCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public HubCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IHubCollectionRequest Request()
    {
      return this.Request(options);
    }

    public IHubCollectionRequest Request(IEnumerable<Option> options)
    {
      return new HubCollectionRequest(this.AppendSegmentToRequestUrl("HubSites"), this.Client, options);
    }
  }
}
