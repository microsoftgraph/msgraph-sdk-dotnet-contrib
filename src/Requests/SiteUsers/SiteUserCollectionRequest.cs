using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteUserCollectionRequest : BaseRequest, ISiteUserCollectionRequest
  {
    public SiteUserCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<ICollectionPage<SPUser>> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<ICollectionPage<SPUser>> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var response = await this.SendAsync<GetCollectionResponse<SPUser>>(null, cancellationToken).ConfigureAwait(false);

      if (response != null && response.Value != null && response.Value.CurrentPage != null)
      {
        return response.Value;
      }
      return null;
    }
  }
}
