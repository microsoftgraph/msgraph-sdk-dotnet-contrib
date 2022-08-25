using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class StorageEntityCollectionRequestBuilder : BaseRequestBuilder, IStorageEntityCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public StorageEntityCollectionRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }


    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    public IStorageEntityCollectionRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public IStorageEntityCollectionRequest Request(IEnumerable<Option> options)
    {
      return new StorageEntityCollectionRequest(this.RequestUrl, this.Client, options);
    }

    /// <summary>
    /// Gets an <see cref="IStorageEntityRequestBuilder"/> for the specified storage entity key.
    /// </summary>
    /// <param name="key">The key for the StorageEntity.</param>
    /// <returns>The <see cref="IStorageEntityRequestBuilder"/>.</returns>
    public IStorageEntityRequestBuilder this[string key]
    {
      get
      {
        if (string.IsNullOrEmpty(key))
        {
          throw new ArgumentNullException(nameof(key));
        }

        // the usual model is to append the id to the query
        // StorageEntities are stored in a single blob, so put it in options for now
        List<QueryOption> options = new List<QueryOption>() { new QueryOption("key", key) };
        return new StorageEntityRequestBuilder(this.RequestUrl, this.Client, options);
      }
    }
  }
}
