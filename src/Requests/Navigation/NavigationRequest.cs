using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class NavigationRequest : BaseSharePointAPIRequest, INavigationRequest
  {
    public NavigationRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("Navigation", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<Navigation> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<Navigation> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<Graph.Community.Navigation>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }

  }
}
