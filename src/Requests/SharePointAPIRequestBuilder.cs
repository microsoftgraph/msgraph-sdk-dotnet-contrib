using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class SharePointAPIRequestBuilder : BaseRequestBuilder, ISharePointAPIRequestBuilder
	{

		public SharePointAPIRequestBuilder(
			Uri siteUrl,
			IBaseClient client)
			: base(siteUrl.ToString(), client)
		{
		}

		public ISiteDesignRequestBuilder SiteDesigns
		{
			get
			{
				return new SiteDesignRequestBuilder(this.AppendSegmentToRequestUrl("_api"), this.Client);
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
	}
}
