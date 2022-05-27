using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListCollectionRequestBuilder : BaseRequestBuilder, IListCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public ListCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IListRequestBuilder this[Guid id]
    {
      get
      {
        if (id == Guid.Empty)
        {
          throw new ArgumentOutOfRangeException(nameof(id));
        }
        return new Graph.Community.ListRequestBuilder(this.AppendSegmentToRequestUrl($"lists('{id}')"), this.Client);
      }
    }

    public IListRequestBuilder this[string title]
    {
      get
      {
        if (string.IsNullOrEmpty(title))
        {
          throw new ArgumentNullException(nameof(title));
        }
        return new Graph.Community.ListRequestBuilder(this.AppendSegmentToRequestUrl($"lists/getByTitle('{title}')"), this.Client);
      }
    }

    public IListCollectionRequest Request()
    {
      return this.Request(options);
    }

    public IListCollectionRequest Request(IEnumerable<Option> options)
    {
      return new ListCollectionRequest(this.AppendSegmentToRequestUrl("lists"), this.Client, options);
    }
  }
}
