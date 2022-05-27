using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListItemCollectionRequestBuilder : BaseRequestBuilder, IListItemCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public ListItemCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public Graph.Community.IListItemRequestBuilder this[int id]
    {
      get
      {
        if (id <1)
        {
          throw new ArgumentOutOfRangeException(nameof(id));
        }
        return new Graph.Community.ListItemRequestBuilder(this.AppendSegmentToRequestUrl($"items({id})"), this.Client);
      }
    }

    public IListItemCollectionRequest Request()
    {
      return this.Request(options);
    }

    public IListItemCollectionRequest Request(IEnumerable<Option> options)
    {
      return new ListItemCollectionRequest(this.AppendSegmentToRequestUrl("items"), this.Client, options);
    }
  }
}
