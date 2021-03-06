using Microsoft.Graph;

namespace Graph.Community
{
  public interface ISharePointAPIRequestBuilder : IBaseRequestBuilder
  {
    ISiteDesignRequestBuilder SiteDesigns { get; }

    ISiteDesignRunRequestBuilder SiteDesignRuns { get; }

    ISiteScriptRequestBuilder SiteScripts { get; }

    ISiteRequestBuilder Site { get; }

    IWebRequestBuilder Web { get; }

    ISearchRequestBuilder Search { get; }
  }
}
