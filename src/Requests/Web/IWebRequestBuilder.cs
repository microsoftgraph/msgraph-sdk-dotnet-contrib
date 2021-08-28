using Microsoft.Graph;

namespace Graph.Community
{
  public interface IWebRequestBuilder : IBaseRequestBuilder
  {
    IWebRequest Request();

    IListRequestBuilder Lists { get; }

    INavigationRequestBuilder Navigation { get; }

    ISiteUserCollectionRequestBuilder SiteUsers { get; }

    ISiteGroupCollectionRequestBuilder SiteGroups { get; }

    IAppTileCollectionRequestBuilder AppTiles { get; }
  }
}
