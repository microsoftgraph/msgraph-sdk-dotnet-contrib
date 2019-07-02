using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public static class GraphServiceExtensions
	{
		public static ISharePointAPIRequestBuilder SharePointAPI(this GraphServiceClient graphServiceClient, Uri siteUrl)
		{
			return new SharePointAPIRequestBuilder(siteUrl, graphServiceClient);
		}
	}
}
