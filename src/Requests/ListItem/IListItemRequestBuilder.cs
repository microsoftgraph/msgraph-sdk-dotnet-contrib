using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListItemRequestBuilder : IBaseRequestBuilder
  {
    IListItemRequest Request();
  }
}
