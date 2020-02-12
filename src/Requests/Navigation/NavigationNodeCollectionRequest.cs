using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  internal class NavigationNodeCollectionRequest : BaseRequest, INavigationNodeCollectionRequest
  {
    internal NavigationNodeCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<ICollectionPage<NavigationNode>> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<ICollectionPage<NavigationNode>> GetAsync(CancellationToken cancellationToken)
    {

      //request.Method = HttpMethod.Get.Method;
      this.ContentType = "application/json";

      var response = await this.SendAsync<GetCollectionResponse<NavigationNode>>(null, cancellationToken).ConfigureAwait(false);

      if (response != null && response.Value != null && response.Value.CurrentPage != null)
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

#pragma warning disable CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA2208 // Instantiate argument exceptions correctly

      if (string.IsNullOrEmpty(creationInformation.Title))
      {
        throw new ArgumentException(paramName: nameof(creationInformation.Title), message: "Title must be provided");
      }
      if (creationInformation.Url == null)
      {
        throw new ArgumentException(paramName: nameof(creationInformation.Url), message: "URL must be provided");
      }

#pragma warning restore CA2208 // Instantiate argument exceptions correctly
#pragma warning restore CA1303 // Do not pass literals as localized parameters

      this.ContentType = "application/json";
      this.Method = System.Net.Http.HttpMethod.Post.Method;
      var newEntity = await this.SendAsync<NavigationNode>(creationInformation, cancellationToken).ConfigureAwait(false);
      return newEntity;

    }
  }
}
