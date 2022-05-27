using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListItemCollectionRequestBuilder
  {
    IListItemCollectionRequest Request();

    IListItemCollectionRequest Request(IEnumerable<Option> options);

    IListItemRequestBuilder this[int id] { get; }
  }
}
