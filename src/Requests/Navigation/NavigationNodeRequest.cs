using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class NavigationNodeRequest : BaseSharePointAPIRequest, INavigationNodeRequest
  {
    public NavigationNodeRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("NavigationNode", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

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

      if (string.IsNullOrEmpty(navigationNode.Title))
      {
        throw new ArgumentException(paramName: nameof(navigationNode.Title), message: "Title must be provided");
      }
      if (navigationNode.Url == null)
      {
        throw new ArgumentException(paramName: nameof(navigationNode.Url), message: "URL must be provided");
      }

      this.ContentType = "application/json";
      this.Method = HttpMethods.POST;
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.XHTTPMethodHeaderName, SharePointAPIRequestConstants.Headers.XHTTPMethodHeaderMergeValue));
      var newEntity = await this.SendAsync<NavigationNode>(navigationNode, cancellationToken).ConfigureAwait(false);
      return newEntity;
    }

    #endregion

  }
}
