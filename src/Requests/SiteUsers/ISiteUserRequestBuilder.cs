using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISiteUserRequestBuilder : IBaseRequestBuilder
  {
    ISiteUserRequest Request();
  }
}
