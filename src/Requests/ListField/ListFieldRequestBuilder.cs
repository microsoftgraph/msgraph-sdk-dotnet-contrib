using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListFieldRequestBuilder: BaseRequestBuilder, IListFieldRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public ListFieldRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IListFieldRequest Request()
    {
      return this.Request(options);
    }

    public IListFieldRequest Request(IEnumerable<Option> options)
    {
      return new Graph.Community.ListFieldRequest(this.RequestUrl, this.Client, options);
    }
  }
}
