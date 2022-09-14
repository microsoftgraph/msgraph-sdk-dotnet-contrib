using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListFieldRequest: BaseSharePointAPIRequest, IListFieldRequest
  {
    public ListFieldRequest(
    string requestUrl,
    IBaseClient client,
    IEnumerable<Option> options)
    : base("ListField", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    public Task<Field> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<Field> GetAsync(CancellationToken cancellationToken)
    {
      this.ContentType = "application/json";
      var entity = await this.SendAsync<Field>(null, cancellationToken).ConfigureAwait(false);
      return entity;
    }

  }
}
