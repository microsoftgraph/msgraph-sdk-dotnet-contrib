using Microsoft.Graph;
using System.Collections.Generic;

namespace Graph.Community
{
  public class NavigationNodeRequestBuilder : BaseRequestBuilder, INavigationNodeRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public NavigationNodeRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null
      )
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public INavigationNodeRequest Request()
    {
      return this.Request(options);
    }

    public INavigationNodeRequest Request(IEnumerable<Option> options)
    {
      return new NavigationNodeRequest(this.RequestUrl, this.Client, options);
    }
  }
}
