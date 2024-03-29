using Microsoft.Graph;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community.Test.Mocks
{
  public class MockSharePointServiceHandler : HttpMessageHandler
  {
    public bool HasOptions { get; set; }
    public SharePointServiceHandlerOption Options;

    private HttpResponseMessage response { get; set; }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      Options = request.GetMiddlewareOption<SharePointServiceHandlerOption>();
      HasOptions = Options != null;
      return await Task.FromResult(response);
    }
  }
}
