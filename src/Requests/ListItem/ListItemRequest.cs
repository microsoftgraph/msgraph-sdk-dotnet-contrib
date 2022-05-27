using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListItemRequest : BaseSharePointAPIRequest, Graph.Community.IListItemRequest
  {
    public ListItemRequest(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options)
        : base("List", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<ListItem> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<ListItem> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<Graph.Community.ListItem>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }

  }
}
