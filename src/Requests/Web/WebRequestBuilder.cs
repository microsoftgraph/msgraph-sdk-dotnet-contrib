using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class WebRequestBuilder : BaseRequestBuilder, IWebRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public WebRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

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

    public ISiteGroupCollectionRequestBuilder SiteGroups
    {
      get
      {
        return new SiteGroupCollectionRequestBuilder(this.AppendSegmentToRequestUrl("sitegroups"), this.Client, this.options);
      }
    }

    public IAppTileCollectionRequestBuilder AppTiles
    {
      get
      {
        return new AppTileCollectionRequestBuilder(this.AppendSegmentToRequestUrl("apptiles"), this.Client, this.options);
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
