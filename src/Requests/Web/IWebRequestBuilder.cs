using Microsoft.Graph;

namespace Graph.Community
{
  public interface IWebRequestBuilder : IBaseRequestBuilder
  {
    IWebRequest Request();

    IAppTileCollectionRequestBuilder AppTiles { get; }

    IListCollectionRequestBuilder Lists { get; }

    INavigationRequestBuilder Navigation { get; }

    ISiteUserCollectionRequestBuilder SiteUsers { get; }

    ISiteGroupCollectionRequestBuilder SiteGroups { get; }
  }
}
