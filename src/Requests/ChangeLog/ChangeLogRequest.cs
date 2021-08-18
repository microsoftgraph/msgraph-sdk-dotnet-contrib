using Microsoft.Graph;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  internal static class ChangeLogRequest
  {
    internal static async Task<IChangeLogCollectionPage> GetChangesAsync(BaseRequest request, ChangeQuery query, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      request.AppendSegmentToRequestUrl("GetChanges");
      request.Method = HttpMethods.POST;
      request.ContentType = "application/json";

      var req = new GetChangesRequest() { Query = query };
      var response = await request.SendAsync<SharePointAPICollectionResponse<IChangeLogCollectionPage>>(req, cancellationToken).ConfigureAwait(false);

      if (response != null && response.Value != null && response.Value.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }
  }
}
