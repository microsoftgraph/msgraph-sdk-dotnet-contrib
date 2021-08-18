using Microsoft.Graph;

namespace Graph.Community
{
  public interface INavigationNodeRequestBuilder : IBaseRequestBuilder
  {
    INavigationNodeRequest Request();
  }
}
