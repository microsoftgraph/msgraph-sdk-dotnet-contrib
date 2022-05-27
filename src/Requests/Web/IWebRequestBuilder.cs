using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IWebRequestBuilder : IBaseRequestBuilder
  {
    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    IWebRequest Request();

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    IWebRequest Request(IEnumerable<Option> options);

    IAppTileCollectionRequestBuilder AppTiles { get; }

    IListCollectionRequestBuilder Lists { get; }

    INavigationRequestBuilder Navigation { get; }

    ISiteUserCollectionRequestBuilder SiteUsers { get; }

    ISiteGroupCollectionRequestBuilder SiteGroups { get; }
  }
}
