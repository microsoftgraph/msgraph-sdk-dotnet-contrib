using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISearchRequestBuilder : IBaseRequestBuilder
  {
    ISearchRequest Request();
  }
}
