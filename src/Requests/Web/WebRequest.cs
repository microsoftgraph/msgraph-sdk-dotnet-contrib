using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public class WebRequest : BaseRequest, IWebRequest
	{
		public WebRequest(
				string requestUrl,
				IBaseClient client,
				IEnumerable<Option> options)
				: base(requestUrl, client, options)
		{
			this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
			this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
		}

		public Task<Web> GetAsync()
		{
			return this.GetAsync(CancellationToken.None);
		}

		public async Task<Web> GetAsync(CancellationToken cancellationToken)
		{
			this.ContentType = "application/json";
			var entity = await this.SendAsync<Graph.Community.Web>(null, cancellationToken).ConfigureAwait(false);
			return entity;
		}

		public Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query)
		{
			return this.GetChangesAsync(query, CancellationToken.None);
		}
		public async Task<ICollectionPage<Change>> GetChangesAsync(ChangeQuery query, CancellationToken cancellationToken)
		{
			return await ChangeLogRequest.GetChangesAsync(this, query, cancellationToken);
		}
	}
}
