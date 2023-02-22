using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageVersionCollectionRequestBuilder: BaseRequestBuilder, ISitePageVersionCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SitePageVersionCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public ISitePageVersionCollectionRequest Request()
    {
      return this.Request(options);
    }

    public ISitePageVersionCollectionRequest Request(IEnumerable<Option> options)
    {
      return new SitePageVersionCollectionRequest(this.RequestUrl, this.Client, options);
    }
  }
}
