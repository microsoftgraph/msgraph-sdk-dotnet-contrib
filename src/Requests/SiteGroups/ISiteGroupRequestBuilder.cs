using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteGroupRequestBuilder : IBaseRequestBuilder
  {
    ISiteGroupRequest Request();
  }
}
