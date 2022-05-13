using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class NavigationNodeCollectionRequestBuilder : BaseRequestBuilder, INavigationNodeCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public NavigationNodeCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null
      )
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public INavigationNodeCollectionRequest Request()
    {
      return this.Request(options);
    }

    public INavigationNodeCollectionRequest Request(IEnumerable<Option> options)
    {
      return new NavigationNodeCollectionRequest(this.RequestUrl, this.Client, options);
    }

  }
}
