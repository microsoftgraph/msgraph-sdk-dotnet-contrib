using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class AppCatalogUrlRequestBuilder:BaseRequestBuilder, IAppCatalogUrlRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public AppCatalogUrlRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IAppCatalogUrlRequest Request()
    {
      return this.Request(options);
    }

    public IAppCatalogUrlRequest Request(IEnumerable<Option> options)
    {
      return new AppCatalogUrlRequest(this.AppendSegmentToRequestUrl("SP_TenantSettings_Current"), this.Client, options);
    }
  }
}
