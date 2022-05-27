using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListItemRequestBuilder:BaseRequestBuilder,IListItemRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public ListItemRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IListItemRequest Request()
    {
      return this.Request(options);
    }

    public Graph.Community.IListItemRequest Request(IEnumerable<Option> options)
    {
      return new Graph.Community.ListItemRequest(this.RequestUrl, this.Client, options);
    }
  }
}
