using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class NavigationRequestBuilder : BaseRequestBuilder, INavigationRequestBuilder
  {
    private IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public NavigationRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null
      )
      : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

    public INavigationRequest Request()
    {
      return this.Request(options);
    }

    public INavigationRequest Request(IEnumerable<Option> options)
    {
      return new NavigationRequest(this.RequestUrl, this.Client, options);
    }

    public INavigationNodeCollectionRequestBuilder QuickLaunch
    {
      get
      {
        return new NavigationNodeCollectionRequestBuilder(this.AppendSegmentToRequestUrl("quicklaunch"), this.Client, options);
      }
    }

    public INavigationNodeCollectionRequestBuilder TopNavigationBar
    {
      get
      {
        return new NavigationNodeCollectionRequestBuilder(this.AppendSegmentToRequestUrl("topnavigationbar"), this.Client, options);
      }
    }

    public INavigationNodeRequestBuilder this[int id]
    {
      get
      {
        return new NavigationNodeRequestBuilder(this.AppendSegmentToRequestUrl($"getbyid({id})"), this.Client, options);
      }
    }
  }
}
