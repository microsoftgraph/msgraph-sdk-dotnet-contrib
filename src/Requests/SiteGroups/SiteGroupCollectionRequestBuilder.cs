using Microsoft.Graph;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteGroupCollectionRequestBuilder : BaseRequestBuilder, ISiteGroupCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteGroupCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public ISiteGroupRequestBuilder this[int id]
    {
      get
      {
        return new SiteGroupRequestBuilder(this.AppendSegmentToRequestUrl($"getbyid({id})"), this.Client, options);
      }
    }

    public ISiteGroupCollectionRequest Request()
    {
      return this.Request(options);
    }

    public ISiteGroupCollectionRequest Request(IEnumerable<Option> options)
    {
      return new SiteGroupCollectionRequest(this.RequestUrl, this.Client, options);
    }
  }
}
