using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListItemCollectionRequest:BaseSharePointAPIRequest, IListItemCollectionRequest
  {

    public ListItemCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("ListItem", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<IListItemCollectionPage> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<IListItemCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var response = await this.SendAsync<SharePointAPICollectionResponse<IListItemCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }

    public Task<ISitePageListItemCollectionPage> GetAsSitePageListItemAsync()
    {
      return GetAsSitePageListItemAsync(CancellationToken.None);
    }

    public async Task<ISitePageListItemCollectionPage> GetAsSitePageListItemAsync(CancellationToken cancellationToken)
    {
      this.QueryOptions.Add(new QueryOption("$expand", "Author,Editor,CheckoutUser"));
      this.QueryOptions.Add(new QueryOption("$select", "Id,Title,Description,Created,Modified,FirstPublishedDate,OData__ModernAudienceTargetUserFieldId,PromotedState,Author/Title,Author/Name,Author/EMail,Editor/Title,Editor/Name,Editor/EMail,CheckoutUser/Title,CheckoutUser/Name,CheckoutUser/EMail"));

      this.ContentType = "application/json";
      var response = await this.SendAsync<SharePointAPICollectionResponse<ISitePageListItemCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }
  }
}
