using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISharePointAPIRequestBuilder : IBaseRequestBuilder
  {
    ISiteDesignCollectionRequestBuilder SiteDesigns { get; }

    ISiteDesignRunRequestBuilder SiteDesignRuns { get; }

    ISiteScriptCollectionRequestBuilder SiteScripts { get; }

    ISiteRequestBuilder Site { get; }

    IWebRequestBuilder Web { get; }

    ISearchRequestBuilder Search { get; }
  }
}
