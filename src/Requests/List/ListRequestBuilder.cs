using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class ListRequestBuilder : BaseRequestBuilder, IListRequestBuilder
  {
    private readonly IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public ListRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
    public IListRequestBuilder this[Guid id]
    {
      get
      {
        return new Graph.Community.ListRequestBuilder(this.AppendSegmentToRequestUrl($"lists('{id.ToString()}')"), this.Client);
      }
    }
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers

    public IListRequestBuilder this[string title]
    {
      get
      {
        return new Graph.Community.ListRequestBuilder(this.AppendSegmentToRequestUrl($"lists/getByTitle('{title}')"), this.Client);
      }
    }

    public IListRequest Request()
    {
      return this.Request(options);
    }

    public IListRequest Request(IEnumerable<Option> options)
    {
      return new ListRequest(this.RequestUrl, this.Client, options);
    }
  }
}
