using System.Net.Http;
using Xunit;

namespace Graph.Community.Test
{
  public class BaseRequestExtensionTests
  {
    [Fact]
    public void GraphRequestWithImmutableIdGeneratesCorrectRequestHeaders()
    {
      // ARRANGE

      using var response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);
      // ACT
      var request = gsc.GraphServiceClient
                          .Me
                          .Request()
                          .WithImmutableId()
                          .GetHttpRequestMessage();

      // ASSERT
      Assert.True(request.Headers.Contains(RequestExtensionsConstants.Headers.PreferHeaderName), $"Header does not contain {RequestExtensionsConstants.Headers.PreferHeaderName} header");
      Assert.Equal(RequestExtensionsConstants.Headers.PreferHeaderImmutableIdValue, string.Join(',', request.Headers.GetValues(RequestExtensionsConstants.Headers.PreferHeaderName)));
    }

    [Fact]
    public void ExtensionRequestGeneratesCorrectRequestHeaders()
    {
      // ARRANGE

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);
      // ACT
      var request = gsc.GraphServiceClient
                          .SharePointAPI("https://mockSite.sharepoint.com")
                          .SiteScripts
                          .Request()
                          .WithImmutableId()
                          .GetHttpRequestMessage();

      // ASSERT
      Assert.True(request.Headers.Contains(RequestExtensionsConstants.Headers.PreferHeaderName), $"Header does not contain {RequestExtensionsConstants.Headers.PreferHeaderName} header");
      Assert.Equal(RequestExtensionsConstants.Headers.PreferHeaderImmutableIdValue, string.Join(',', request.Headers.GetValues(RequestExtensionsConstants.Headers.PreferHeaderName)));
    }

    [Fact]
    public void GraphRequestWithEventualConsistencyGeneratesCorrectRequestHeader()
    {
      // ARRANGE

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      var request = gsc.GraphServiceClient
                          .Users
                          .Request()
                          .WithEventualConsistency()
                          .GetHttpRequestMessage();

      // ASSERT
      Assert.True(request.Headers.Contains(RequestExtensionsConstants.Headers.ConsistencyLevelHeaderName), $"Header does not contain {RequestExtensionsConstants.Headers.ConsistencyLevelHeaderName} header");
      Assert.Equal(RequestExtensionsConstants.Headers.ConsistencyLevelEventualValue, string.Join(',', request.Headers.GetValues(RequestExtensionsConstants.Headers.ConsistencyLevelHeaderName)));

    }
  }
}
