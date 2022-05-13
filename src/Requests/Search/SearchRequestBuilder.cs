using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SearchRequestBuilder : BaseRequestBuilder, ISearchRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SearchRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public ISearchRequest Request()
    {
      return this.Request(options);
    }

    public ISearchRequest Request(IEnumerable<Option> options)
    {
      return new SearchRequest(this.RequestUrl, this.Client, options);
    }

  }
}
