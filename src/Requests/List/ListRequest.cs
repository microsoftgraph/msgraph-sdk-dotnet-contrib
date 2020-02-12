using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class ListRequest : BaseRequest, IListRequest
  {
#pragma warning disable CA1054 // URI parameters should not be strings
    public ListRequest(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options)
        : base(requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }
#pragma warning restore CA1054 // URI parameters should not be strings

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

    public Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query)
    {
      return this.GetChangesAsync(query, CancellationToken.None);
    }
    public async Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken)
    {
      return await ChangeLogRequest.GetChangesAsync(this, query, cancellationToken).ConfigureAwait(false);
    }
  }
}
