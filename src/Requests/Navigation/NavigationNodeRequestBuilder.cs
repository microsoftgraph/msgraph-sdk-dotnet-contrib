using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class NavigationNodeRequestBuilder : BaseRequestBuilder, INavigationNodeRequestBuilder
  {
    private IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public NavigationNodeRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null
      )
      : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

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
