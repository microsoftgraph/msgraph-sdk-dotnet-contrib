using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class StorageEntityRequestBuilder : BaseRequestBuilder, IStorageEntityRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public StorageEntityRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IStorageEntityRequest Request()
    {
      return this.Request(options);
    }

    public IStorageEntityRequest Request(IEnumerable<Option> options)
    {
      return new StorageEntityRequest(this.AppendSegmentToRequestUrl("web/AllProperties?$select=storageentitiesindex"), this.Client, options);
    }
  }
}
