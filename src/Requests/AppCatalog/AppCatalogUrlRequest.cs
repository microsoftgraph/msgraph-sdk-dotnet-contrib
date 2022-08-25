using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Graph;
using System.Threading.Tasks;
using System.Threading;
using Azure;

namespace Graph.Community
{
  public class AppCatalogUrlRequest:BaseSharePointAPIRequest, IAppCatalogUrlRequest
  {
    public AppCatalogUrlRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("AppCatalogUrl", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    #region Get

    public Task<string> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<string> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var settings = await this.SendAsync<TenantSettingsResponse>(null, cancellationToken).ConfigureAwait(false);

      return settings?.CorporateCatalogUrl;
    }

    #endregion


  }
}
