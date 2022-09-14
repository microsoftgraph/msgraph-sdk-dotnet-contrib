using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListFieldRequestBuilder: IBaseRequestBuilder
  {
    IListFieldRequest Request();
    IListFieldRequest Request(IEnumerable<Option> options);
  }
}
