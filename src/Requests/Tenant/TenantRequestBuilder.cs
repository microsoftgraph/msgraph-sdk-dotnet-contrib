using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class TenantRequestBuilder : BaseRequestBuilder, ITenantRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public TenantRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IAppCatalogUrlRequestBuilder AppCatalogUrl
    {
      get
      {
        return new AppCatalogUrlRequestBuilder(this.RequestUrl, this.Client, this.options);
      }
    }

    public IStorageEntityCollectionRequestBuilder StorageEntities
    {
      get
      {
        return new StorageEntityCollectionRequestBuilder(this.RequestUrl, this.Client, this.options);
      }
    }
  }
}
