using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class NavigationNodeCollectionRequestBuilder : BaseRequestBuilder, INavigationNodeCollectionRequestBuilder
  {
    private IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public NavigationNodeCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null
      )
      : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

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
