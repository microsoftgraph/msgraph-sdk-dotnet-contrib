using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IHubCollectionRequestBuilder:IBaseRequestBuilder
  {
    IHubCollectionRequest Request();

    IHubCollectionRequest Request(IEnumerable<Option> options);
  }
}
