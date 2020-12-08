using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteUserRequest : BaseSharePointAPIRequest, ISiteUserRequest
  {
    public SiteUserRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SiteUser", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<User> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<User> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<User>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }
  }
}
