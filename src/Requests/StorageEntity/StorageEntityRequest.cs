using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Graph;
using System.Threading.Tasks;
using System.Threading;
using Azure;

namespace Graph.Community
{
  public class StorageEntityRequest : BaseSharePointAPIRequest, IStorageEntityRequest
  {
    public StorageEntityRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("StorageEntity", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
    }

    #region Get

    public Task<StorageEntity> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<StorageEntity> GetAsync(CancellationToken cancellationToken)
    {
      // the usual model is to append the id to the query
      // StorageEntities are stored in a single blob, so grab it from options 
      var keyOption = this.QueryOptions.First(o => o.Name.Equals("key", StringComparison.InvariantCultureIgnoreCase));
      this.QueryOptions.Remove(keyOption);

      var response = await this.SendAsync<StorageEntitiesResponse>(null, cancellationToken).ConfigureAwait(false);

      if (!string.IsNullOrEmpty(response?.StorageEntitiesIndex))
      {
        var entities = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, StorageEntity>>(response.StorageEntitiesIndex);
        return entities[keyOption.Value];
      }

      return null;
    }

    #endregion


  }
}
