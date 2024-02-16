using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class HubRequest:BaseSharePointAPIRequest, IHubRequest
  {
    public HubRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("Hub", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<Hub> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<Hub> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<Hub>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }
  }
}
