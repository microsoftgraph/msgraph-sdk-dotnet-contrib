using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class AppTileCollectionRequestBuilder : BaseRequestBuilder, IAppTileCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public AppTileCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IAppTileCollectionRequest Request()
    {
      return this.Request(options);
    }

    public IAppTileCollectionRequest Request(IEnumerable<Option> options)
    {
      return new AppTileCollectionRequest(this.RequestUrl, this.Client, options);
    }
  }
}
