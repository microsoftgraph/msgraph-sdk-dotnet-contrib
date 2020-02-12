using Moq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task

  public class ListRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public ListRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GetById_GeneratesCorrectRequestUriAndHeaders()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/lists('{mockListId.ToString()}')");

      using (var response = new HttpResponseMessage())
      using (var testClient = GraphServiceTestClient.Create(response))
      {
        // ACT
        var request = testClient.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .Web
                            .Lists[mockListId]
                            .Request()
                            .GetHttpRequestMessage();

        // ASSERT
        Assert.Equal(expectedUri, request.RequestUri);
        Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
        Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
        Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
      }
    }

    [Fact]
    public void GetByTitle_GeneratesCorrectRequestUriAndHeaders()
    {
      // ARRANGE
      var mockListTitle = "mockListTitle";
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/lists/getByTitle('{mockListTitle}')");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .Web
                            .Lists[mockListTitle]
                            .Request()
                            .GetHttpRequestMessage();

        // ASSERT
        Assert.Equal(expectedUri, request.RequestUri);
        Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
        Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
        Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
      }
    }

    [Fact]
    public async Task GetChanges_GeneratesCorrectRequest()
    {
      // ARRANGE
      var query = new ChangeQuery()
      {
        Add = true
      };
      var mockListId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/lists('{mockListId.ToString()}')/GetChanges");
      var expectedContent = "{\"query\":{\"Add\":true}}";

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Lists[mockListId]
                    .Request()
                    .GetChangesAsync(query);
        var actualContent = gsc.HttpProvider.ContentAsString;

        // ASSERT
        gsc.HttpProvider.Verify(
          provider => provider.SendAsync(
            It.Is<HttpRequestMessage>(req =>
              req.Method == HttpMethod.Post &&
              req.RequestUri == expectedUri &&
              req.Headers.Authorization != null
            ),
            It.IsAny<HttpCompletionOption>(),
            It.IsAny<CancellationToken>()
          ),
          Times.Exactly(1)
        );

        Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, gsc.HttpProvider.ContentHeaders.ContentType.MediaType);
        Assert.Equal(expectedContent, actualContent);
      }
    }
  }

#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
#pragma warning restore CA1707
}
