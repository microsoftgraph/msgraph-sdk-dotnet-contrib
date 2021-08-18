using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteGroupCollectionRequest : BaseSharePointAPIRequest, ISiteGroupCollectionRequest
  {
    public SiteGroupCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SiteGroupCollection", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public async Task<ISiteGroupCollectionPage> GetAsync()
    {
      return await this.GetAsync(CancellationToken.None);
    }

    public async Task<ISiteGroupCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var response = await this.SendAsync<SharePointAPICollectionResponse<ISiteGroupCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }

    public ISiteGroupCollectionRequest Expand(string value)
    {
      this.QueryOptions.Add(new QueryOption("$expand", value));
      return this;
    }

    public ISiteGroupCollectionRequest Expand(Expression<Func<Group, object>> expandExpression)
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
