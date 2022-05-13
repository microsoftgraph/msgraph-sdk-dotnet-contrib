using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SiteUserCollectionRequestBuilder : BaseRequestBuilder, ISiteUserCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteUserCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public ISiteUserRequestBuilder this[int id]
    {
      get
      {
        return new SiteUserRequestBuilder(this.AppendSegmentToRequestUrl($"getbyid({id})"), this.Client, options);
      }
    }

    public ISiteUserCollectionRequest Request()
    {
      return this.Request(options);
    }

    public ISiteUserCollectionRequest Request(IEnumerable<Option> options)
    {
      return new SiteUserCollectionRequest(this.RequestUrl, this.Client, options);
    }
  }
}
