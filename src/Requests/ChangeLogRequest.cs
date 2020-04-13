using Microsoft.Graph;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  internal static class ChangeLogRequest
  {
    internal static async Task<ICollectionPage<Change>> GetChangesAsync(BaseRequest request, ChangeQuery query, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      request.AppendSegmentToRequestUrl("GetChanges");
      request.Method = HttpMethod.Post.Method;
      request.ContentType = "application/json";

      var req = new GetChangesRequest() { Query = query };
      var response = await request.SendAsync<GetCollectionResponse<Change>>(req, cancellationToken).ConfigureAwait(false);

      if (response != null && response.Value != null && response.Value.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }
  }
}
