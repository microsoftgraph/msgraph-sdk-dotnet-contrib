using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListRequest : BaseSharePointAPIRequest, IListRequest
  {
    public ListRequest(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options)
        : base("List", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<List> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<List> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<Graph.Community.List>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }

    public IListRequest Expand(string value)
    {
      this.QueryOptions.Add(new QueryOption("$expand", value));
      return this;
    }

    public IListRequest Expand(Expression<Func<List, object>> expandExpression)
    {
      if (expandExpression == null)
      {
        throw new ArgumentNullException(nameof(expandExpression));
      }
      string value = ExpressionExtractHelper.ExtractMembers(expandExpression, out string error);
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


    public Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query)
    {
      return this.GetChangesAsync(query, CancellationToken.None);
    }
    public async Task<IChangeLogCollectionPage> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken)
    {
      return await ChangeLogRequest.GetChangesAsync(this, query, cancellationToken).ConfigureAwait(false);
    }
  }
}
