using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class ListRequestBuilder : BaseRequestBuilder, IListRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public ListRequestBuilder(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options = null)
      : base(requestUrl, client)
    {
      this.options = options;
    }

    public IListItemCollectionRequestBuilder Items 
    { 
      get
      {
        return new ListItemCollectionRequestBuilder(this.RequestUrl, this.Client);
      }
    }

    public IListFieldCollectionRequestBuilder Fields
    {
      get
      {
        return new ListFieldCollectionRequestBuilder(this.RequestUrl, this.Client);
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
