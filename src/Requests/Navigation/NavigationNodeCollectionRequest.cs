using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  internal class NavigationNodeCollectionRequest : BaseSharePointAPIRequest, INavigationNodeCollectionRequest
  {
    internal NavigationNodeCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("NavigationNodeCollection", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<INavigationNodeCollectionPage> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<INavigationNodeCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";

      var response = await this.SendAsync<SharePointAPICollectionResponse<INavigationNodeCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }

    public Task<NavigationNode> AddAsync(NavigationNodeCreationInformation creationInformation)
    {
      return this.AddAsync(creationInformation, CancellationToken.None);
    }

    public async Task<NavigationNode> AddAsync(NavigationNodeCreationInformation creationInformation, CancellationToken cancellationToken)
    {
      if (creationInformation == null)
      {
        throw new ArgumentNullException(nameof(creationInformation));
      }

      if (string.IsNullOrEmpty(creationInformation.Title))
      {
        throw new ArgumentException(paramName: nameof(creationInformation.Title), message: "Title must be provided");
      }
      if (creationInformation.Url == null)
      {
        throw new ArgumentException(paramName: nameof(creationInformation.Url), message: "URL must be provided");
      }

      this.ContentType = "application/json";
      this.Method = HttpMethods.POST;
      var newEntity = await this.SendAsync<NavigationNode>(creationInformation, cancellationToken).ConfigureAwait(false);
      return newEntity;

    }
  }
}
