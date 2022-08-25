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

    ISitePageCollectionRequestBuilder SitePages { get; }

    ISearchRequestBuilder Search { get; }

    ITenantRequestBuilder Tenant { get; } 
  }
}
