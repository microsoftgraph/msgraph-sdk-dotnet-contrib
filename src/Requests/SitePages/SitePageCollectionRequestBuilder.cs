using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageCollectionRequestBuilder : BaseRequestBuilder, ISitePageCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SitePageCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public ISitePageRequestBuilder this[string name]
    {
      get
      {
        if (string.IsNullOrEmpty(name))
        {
          throw new ArgumentNullException(nameof(name));
        }

        /*
         * We need the server-relative url.
         * We know that the request url is `_api` in the appropriate site. So grab all the path segments before `_api`
         */

        List<string> serverRelativeUrlSegments = new List<string>();

        var u = new Uri(this.RequestUrl);
        foreach (var segment in u.Segments)
        {
          if (segment != "_api")
          {
            serverRelativeUrlSegments.Add(segment);
          }
          else
          {
            break;
          }
        }

        serverRelativeUrlSegments.Add("SitePages/");
        serverRelativeUrlSegments.Add(WebUtility.UrlEncode(name));
        var serverRelativeUrl = string.Join("", serverRelativeUrlSegments);

        // and we need some expand params...
        List<Option> opts = new List<Option>()
        {
          new QueryOption("$expand","ListItemAllFields/ClientSideApplicationId,ListItemAllFields/PageLayoutType,ListItemAllFields/CommentsDisabled")
        };
        return new SitePageRequestBuilder(this.AppendSegmentToRequestUrl($"web/getfilebyserverrelativeurl('{serverRelativeUrl}')"), this.Client, opts);
      }
    }

    public ISitePageCollectionRequest Request()
    {
      return this.Request(options);
    }

    public ISitePageCollectionRequest Request(IEnumerable<Option> options)
    {
      this.RequestUrl.Replace("web", "");
      return new SitePageCollectionRequest(this.AppendSegmentToRequestUrl("sitepages/pages"), this.Client, options);
    }
  }
}
