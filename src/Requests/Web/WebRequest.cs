using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class WebRequest : BaseSharePointAPIRequest, IWebRequest
  {
    public WebRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("Site", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

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

    public Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query)
    {
      return this.GetChangesAsync(query, CancellationToken.None);
    }
    public async Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken)
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
      this.Method = HttpMethods.POST;
      this.ContentType = "application/json";

      var payload = new { logonName };
      var userEntity = await this.SendAsync<User>(payload, cancellationToken);
      return userEntity;
    }

    public async Task<Web> GetAssociatedGroupsAsync()
    {
      return await GetAssociatedGroupsAsync(false, CancellationToken.None);
    }

    public async Task<Web> GetAssociatedGroupsAsync(bool includeUsers = false)
    {
      return await GetAssociatedGroupsAsync(includeUsers, CancellationToken.None);
    }

    public async Task<Web> GetAssociatedGroupsAsync(bool includeUsers, CancellationToken cancellationToken)
    {
      string expand = "associatedownergroup,associatedmembergroup,associatedvisitorgroup";
      if (includeUsers)
      {
        expand += ",associatedownergroup/users,associatedmembergroup/users,associatedvisitorgroup/users";
      }
      this.QueryOptions.Add(new QueryOption("$expand", expand));
      this.QueryOptions.Add(new Microsoft.Graph.QueryOption("$select", "id,title,associatedownergroup,associatedmembergroup,associatedvisitorgroup"));

      var web = await this.SendAsync<Web>(null, cancellationToken);
      return web;
    }

    public async Task<List> GetSitePagesListAsync()
    {
      return await this.GetSitePagesListAsync(CancellationToken.None);
    }
    public async Task<List> GetSitePagesListAsync(CancellationToken cancellationToken)
    {
      this.QueryOptions.Add(new QueryOption("$expand", "Lists"));
      this.QueryOptions.Add(new QueryOption("$select", "Id,Title,Lists/Id,Lists/BaseTemplate"));

      var web = await this.SendAsync<Web>(null, cancellationToken);
      var adLists = web.AdditionalData["Lists"];
      if (adLists != null && 
          adLists is System.Text.Json.JsonElement listsElement &&
          listsElement.ValueKind == System.Text.Json.JsonValueKind.Array)
      {
        var lists = listsElement.Deserialize<List<Graph.Community.List>>();
        return lists.FirstOrDefault(l => l.BaseTemplate.Equals(119));
      }
      return null;
    }

    public Task<List> EnsureSiteAssetsAsync()
    {
      return this.EnsureSiteAssetsAsync(CancellationToken.None);
    }

    public async Task<List> EnsureSiteAssetsAsync(CancellationToken cancellationToken)
    {
      this.AppendSegmentToRequestUrl("Lists/EnsureSiteAssetsLibrary");
      this.Method = HttpMethods.POST;

      var entity = await this.SendAsync<Graph.Community.List>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }

    public IWebRequest Expand(string value)
    {
      this.QueryOptions.Add(new QueryOption("$expand", value));
      return this;
    }

    public IWebRequest Expand(Expression<Func<Group, object>> expandExpression)
    {
      if (expandExpression == null)
      {
        throw new ArgumentNullException(nameof(expandExpression));
      }
      string error;
      string value = ExpressionExtractHelper.ExtractMembers(expandExpression, out error);
      if (value == null)
      {
        throw new ArgumentException(error, nameof(expandExpression));
      }
      else
      {
        this.QueryOptions.Add(new QueryOption("$expand", value));
      }
      return this;
    }
  
  }
}
