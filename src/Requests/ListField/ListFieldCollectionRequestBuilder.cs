using System;
using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListFieldCollectionRequestBuilder: BaseRequestBuilder, IListFieldCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public ListFieldCollectionRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IListFieldRequestBuilder this[string id]
    {
      get
      {
        if (string.IsNullOrEmpty(id))
        {
          throw new ArgumentNullException("id");
        }
        return new ListFieldRequestBuilder(this.AppendSegmentToRequestUrl($"fields({id})"), this.Client);
      }
    }

    public IListFieldCollectionRequest Request()
    {
      return this.Request(options);
    }

    public IListFieldCollectionRequest Request(IEnumerable<Option> options)
    {
      return new ListFieldCollectionRequest(this.AppendSegmentToRequestUrl("fields"), this.Client, options);
    }

  }
}
