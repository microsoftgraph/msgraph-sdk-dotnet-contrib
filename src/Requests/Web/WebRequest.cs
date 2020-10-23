using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class WebRequest : BaseRequest, IWebRequest
  {
#pragma warning disable CA1054 // URI parameters should not be strings
    public WebRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }
#pragma warning restore CA1054 // URI parameters should not be strings

    public Task<Web> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<Web> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<Graph.Community.Web>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }

    public Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query)
    {
      return this.GetChangesAsync(query, CancellationToken.None);
    }
    public async Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken)
    {
      return await ChangeLogRequest.GetChangesAsync(this, query, cancellationToken).ConfigureAwait(false);
    }

    public async Task<User> EnsureUserAsync(string logonName)
    {
      return await this.EnsureUserAsync(logonName, CancellationToken.None);
    }

    public async Task<User> EnsureUserAsync(string logonName, CancellationToken cancellationToken)
    {
      if (string.IsNullOrEmpty(logonName))
      {
        throw new ArgumentNullException(nameof(logonName));
      }

      this.AppendSegmentToRequestUrl("ensureuser");
      this.Method = HttpMethod.Post.Method;
      this.ContentType = "application/json";

      var payload = new { logonName = logonName };
      var userEntity = await this.SendAsync<User>(payload, cancellationToken);
      return userEntity;
    }

    public async Task<Web> GetAssociatedGroupsAsync()
    {
      return await GetAssociatedGroupsAsync(CancellationToken.None);
    }

    public async Task<Web> GetAssociatedGroupsAsync(CancellationToken cancellationToken)
    {
      this.QueryOptions.Add(new QueryOption("$expand", "associatedownergroup,associatedmembergroup,associatedvisitorgroup"));
      this.QueryOptions.Add(new Microsoft.Graph.QueryOption("$select", "associatedownergroup,associatedmembergroup,associatedvisitorgroup"));

      var web = await this.SendAsync<Web>(null, cancellationToken);
      return web;
    }
  }
}
