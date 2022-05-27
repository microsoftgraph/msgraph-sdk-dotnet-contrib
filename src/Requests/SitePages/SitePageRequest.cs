using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageRequest : BaseSharePointAPIRequest, ISitePageRequest
  {
    public SitePageRequest(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options)
        : base("SitePage", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<SitePageFileInfo> GetAsync()
    {
     return this.GetAsync(CancellationToken.None);
    }

    public async Task<SitePageFileInfo> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<SitePageFileInfo>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }
  }
}
