using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class NavigationNodeRequest : BaseRequest, INavigationNodeRequest
  {
#pragma warning disable CA1054 // URI parameters should not be strings
    public NavigationNodeRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }
#pragma warning restore CA1054 // URI parameters should not be strings

    #region Get
    public Task<NavigationNode> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<NavigationNode> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<NavigationNode>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }

    #endregion

    #region Update

    public Task<NavigationNode> UpdateAsync(NavigationNode navigationNode)
    {
      return this.UpdateAsync(navigationNode, CancellationToken.None);
    }

    public async Task<NavigationNode> UpdateAsync(NavigationNode navigationNode, CancellationToken cancellationToken)
    {
      if (navigationNode == null)
      {
        throw new ArgumentNullException(nameof(navigationNode));
      }

#pragma warning disable CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA2208 // Instantiate argument exceptions correctly

      if (string.IsNullOrEmpty(navigationNode.Title))
      {
        throw new ArgumentException(paramName: nameof(navigationNode.Title), message: "Title must be provided");
      }
      if (navigationNode.Url == null)
      {
        throw new ArgumentException(paramName: nameof(navigationNode.Url), message: "URL must be provided");
      }

#pragma warning restore CA2208 // Instantiate argument exceptions correctly
#pragma warning restore CA1303 // Do not pass literals as localized parameters

      this.ContentType = "application/json";
      this.Method = System.Net.Http.HttpMethod.Post.Method;
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.XHTTPMethodHeaderName, SharePointAPIRequestConstants.Headers.XHTTPMethodHeaderMergeValue));
      var newEntity = await this.SendAsync<NavigationNode>(navigationNode, cancellationToken).ConfigureAwait(false);
      return newEntity;
    }

    #endregion

  }
}
