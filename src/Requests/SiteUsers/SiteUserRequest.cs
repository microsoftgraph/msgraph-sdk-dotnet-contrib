using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteUserRequest : BaseRequest, ISiteUserRequest
  {
    public SiteUserRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<SPUser> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<SPUser> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<SPUser>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }
  }
}
