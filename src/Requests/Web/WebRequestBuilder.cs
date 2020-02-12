using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class WebRequestBuilder : BaseRequestBuilder, IWebRequestBuilder
  {
    private IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public WebRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

    public IListRequestBuilder Lists
    {
      get
      {
        return new ListRequestBuilder(this.RequestUrl, this.Client, this.options);
      }
    }

    public INavigationRequestBuilder Navigation
    {
      get
      {
        return new NavigationRequestBuilder(this.AppendSegmentToRequestUrl("navigation"), this.Client, this.options);
      }
    }

    public ISiteUserCollectionRequestBuilder SiteUsers
    {
      get
      {
        return new SiteUserCollectionRequestBuilder(this.AppendSegmentToRequestUrl("siteusers"), this.Client, this.options);
      }
    }

    public IWebRequest Request()
    {
      return this.Request(options);
    }

    public IWebRequest Request(IEnumerable<Option> options)
    {
      return new WebRequest(this.RequestUrl, this.Client, options);
    }
  }
}
