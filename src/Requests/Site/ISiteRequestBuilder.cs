using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteRequestBuilder : IBaseRequestBuilder
  {
    ISiteRequest Request();
  }
}
