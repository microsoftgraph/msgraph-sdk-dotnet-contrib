using Microsoft.Graph;

namespace Graph.Community
{
  public interface INavigationNodeCollectionRequestBuilder : IBaseRequestBuilder
  {
    INavigationNodeCollectionRequest Request();
  }
}
