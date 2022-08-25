using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;
using System.Threading.Tasks;
using System.Threading;

namespace Graph.Community
{
  public class StorageEntityCollectionRequest : BaseSharePointAPIRequest, IStorageEntityCollectionRequest
  {
    public StorageEntityCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("StorageEntity", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
      this.Method = HttpMethods.POST;
    }

    #region Get

    public Task<Dictionary<string, StorageEntity>> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<Dictionary<string, StorageEntity>> GetAsync(CancellationToken cancellationToken)
    {
      this.AppendSegmentToRequestUrl("web/AllProperties?$select=storageentitiesindex");

      var response = await this.SendAsync<StorageEntitiesResponse>(null, cancellationToken).ConfigureAwait(false);

      if (!string.IsNullOrEmpty(response?.StorageEntitiesIndex))
      {
        var result = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, StorageEntity>>(response.StorageEntitiesIndex);
        return result;
      }

      return null;
    }

    #endregion


  }
}
