using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListCollectionRequest : BaseSharePointAPIRequest, IListCollectionRequest
  {
    public ListCollectionRequest(
      string requestUrl, 
      IBaseClient client, 
      IEnumerable<Option> options) 
      : base("List", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<IListCollectionPage> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<IListCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var response = await this.SendAsync<SharePointAPICollectionResponse<IListCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }

    public IListCollectionRequest Expand(string value)
    {
      this.QueryOptions.Add(new QueryOption("$expand", value));
      return this;
    }

    public IListCollectionRequest Expand(Expression<Func<List, object>> expandExpression)
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

  }
}
