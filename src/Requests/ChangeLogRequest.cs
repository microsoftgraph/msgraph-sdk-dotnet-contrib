using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public static class ChangeLogRequest 
	{
		public static async Task<ICollectionPage<Change>> GetChangesAsync(BaseRequest request, ChangeQuery query, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			request.AppendSegmentToRequestUrl("GetChanges");
			request.Method = HttpMethod.Post.Method;
			request.ContentType = "application/json";

			var req = new GetChangesRequest() { Query = query };
			var response = await request.SendAsync<GetChangesResponse>(req, cancellationToken).ConfigureAwait(false);

			if (response != null && response.Value != null && response.Value.CurrentPage != null)
			{
				return response.Value;
			}

			return null;
		}
	}
}
