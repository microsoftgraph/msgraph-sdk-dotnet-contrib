using System;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListRequestBuilder : IBaseRequestBuilder
  {
    public IListItemCollectionRequestBuilder Items { get; }

    IListRequest Request();
  }
}
