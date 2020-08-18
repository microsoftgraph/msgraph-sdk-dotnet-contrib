using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public class SiteDesignRunRequest : BaseRequest, ISiteDesignRunRequest
	{

		public SiteDesignRunRequest(
			string requestUrl,
			IBaseClient client,
			IEnumerable<Option> options)
			: base(requestUrl, client, options)
		{
			this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
			this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
			this.Method = System.Net.Http.HttpMethod.Post.Method;
		}

		public Task<ICollectionPage<SiteDesignRun>> GetAsync()
		{
			return this.GetAsync(CancellationToken.None);
		}

		public async Task<ICollectionPage<SiteDesignRun>> GetAsync(CancellationToken cancellationToken)
		{
			GetSiteDesignRunCollectionResponse response = new GetSiteDesignRunCollectionResponse();

			this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesignRun");

			response = await this.SendAsync<GetSiteDesignRunCollectionResponse>(null, cancellationToken).ConfigureAwait(false);

			if (response != null && response.Value != null && response.Value.CurrentPage != null)
			{
				return response.Value;
			}

			return null;
		}

	}
}
