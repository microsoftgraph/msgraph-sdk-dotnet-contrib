using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class AppTileCollectionRequest : BaseSharePointAPIRequest, IAppTileCollectionRequest
  {
    public AppTileCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("AppTileCollection", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<ICollectionPage<AppTile>> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<ICollectionPage<AppTile>> GetAsync(CancellationToken cancellationToken)
    {
      var response = await this.SendAsync<GetCollectionResponse<AppTile>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }
      return null;
    }

    public IAppTileCollectionRequest OrderBy(string value)
    {
      this.QueryOptions.Add(new QueryOption("$orderby", value));
      return this;
    }

    public IAppTileCollectionRequest Select(string value)
    {
      this.QueryOptions.Add(new QueryOption("$select", value));
      return this;
    }

    public IAppTileCollectionRequest Select(Expression<Func<AppTile, object>> selectExpression)
    {
      if (selectExpression == null)
      {
        throw new ArgumentNullException(nameof(selectExpression));
      }
      string error;
      string value = ExpressionExtractHelper.ExtractMembers(selectExpression, out error);
      if (value == null)
      {
        throw new ArgumentException(error, nameof(selectExpression));
      }
      else
      {
        this.QueryOptions.Add(new QueryOption("$select", value));
      }
      return this;
    }

    public IAppTileCollectionRequest Skip(int value)
    {
      this.QueryOptions.Add(new QueryOption("$skip", value.ToString()));
      return this;
    }

    public IAppTileCollectionRequest Top(int value)
    {
      this.QueryOptions.Add(new QueryOption("$top", value.ToString()));
      return this;
    }
  }
}
