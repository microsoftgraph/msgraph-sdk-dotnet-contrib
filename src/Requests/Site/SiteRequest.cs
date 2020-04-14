using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteRequest : BaseRequest, Graph.Community.ISiteRequest
  {
#pragma warning disable CA1054 // URI parameters should not be strings
    public SiteRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {

      // TODO:  Consider moving this to a Community base request object...

      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }
#pragma warning restore CA1054 // URI parameters should not be strings

    public Task<Site> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<Site> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<Graph.Community.Site>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }
  }
}
