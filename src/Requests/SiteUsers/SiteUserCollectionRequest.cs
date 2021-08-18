using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteUserCollectionRequest : BaseSharePointAPIRequest, ISiteUserCollectionRequest
  {
    public SiteUserCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SiteUserCollection", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<ISiteUserCollectionPage> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<ISiteUserCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var response = await this.SendAsync<SharePointAPICollectionResponse<ISiteUserCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }
  }
}
