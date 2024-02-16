using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class HubCollectionRequest : BaseSharePointAPIRequest, IHubCollectionRequest
  {
    public HubCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SitePages", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<IHubCollectionPage> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<IHubCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var response = await this.SendAsync<SharePointAPICollectionResponse<IHubCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }
  }
}
