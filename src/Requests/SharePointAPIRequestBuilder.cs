using Microsoft.Graph;

namespace Graph.Community
{
	public class SharePointAPIRequestBuilder : BaseRequestBuilder, ISharePointAPIRequestBuilder
	{
		public SharePointAPIRequestBuilder(
			string siteUrl,
			IBaseClient client)
			: base(siteUrl, client)
		{
		}

		public ISiteDesignCollectionRequestBuilder SiteDesigns
		{
			get
			{
				return new SiteDesignCollectionRequestBuilder(this.AppendSegmentToRequestUrl("_api"), this.Client);
			}
		}

		public ISiteDesignRunRequestBuilder SiteDesignRuns
		{
			get
			{
				return new SiteDesignRunRequestBuilder(this.AppendSegmentToRequestUrl("_api"), this.Client);
			}
		}

		public ISiteScriptCollectionRequestBuilder SiteScripts
		{
			get
			{
				return new SiteScriptCollectionRequestBuilder(this.AppendSegmentToRequestUrl("_api"), this.Client);
			}
		}
		public Graph.Community.ISiteRequestBuilder Site
		{
			get
			{
				return new Graph.Community.SiteRequestBuilder(this.AppendSegmentToRequestUrl("_api/site"), this.Client);
			}
		}

		public Graph.Community.IWebRequestBuilder Web
		{
			get
			{
				return new Graph.Community.WebRequestBuilder(this.AppendSegmentToRequestUrl("_api/web"), this.Client);
			}
		}

		public Graph.Community.ISearchRequestBuilder Search
		{
			get
			{
				return new Graph.Community.SearchRequestBuilder(this.AppendSegmentToRequestUrl("_api/search"), this.Client);
			}
		}
	}
}
