using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task

  public class WebRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public WebRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      // TODO: move this to a base test class...

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .Web
                            .Request()
                            .GetHttpRequestMessage();

        // ASSERT
        Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
        Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
        Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
      }
    }

    [Fact]
    public async Task Get_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Request()
                    .GetAsync();

        // ASSERT
        gsc.HttpProvider.Verify(
          provider => provider.SendAsync(
            It.Is<HttpRequestMessage>(req =>
              req.Method == HttpMethod.Get &&
              req.RequestUri == expectedUri &&
              req.Headers.Authorization != null
            ),
            It.IsAny<HttpCompletionOption>(),
            It.IsAny<CancellationToken>()
            ),
          Times.Exactly(1)
        );
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
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/GetChanges");
      var expectedContent = "{\"query\":{\"Add\":true}}";

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
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

    [Fact]
    public async Task GetSiteUsers_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/siteusers");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .SiteUsers
                    .Request()
                    .GetAsync();

        // ASSERT
        gsc.HttpProvider.Verify(
          provider => provider.SendAsync(
            It.Is<HttpRequestMessage>(req =>
              req.Method == HttpMethod.Get &&
              req.RequestUri == expectedUri &&
              req.Headers.Authorization != null
            ),
            It.IsAny<HttpCompletionOption>(),
            It.IsAny<CancellationToken>()
          ),
          Times.Exactly(1)
        );
      }
    }

    [Fact]
    public async Task GetSiteUsers_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetSiteUsersResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };


      using (responseMessage)
      using (var gsc = GraphServiceTestClient.Create(responseMessage))
      {
        // ACT
        var response = await gsc.GraphServiceClient
                        .SharePointAPI(mockWebUrl)
                        .Web
                        .SiteUsers
                        .Request()
                        .GetAsync();
        var actual = response;

        // ASSERT
        Assert.Equal(8, actual.Count);
        Assert.Equal(12, actual[5].Id);
        Assert.False(actual[5].IsSiteAdmin);
        Assert.Equal("Megan Bowen", actual[5].Title);
        Assert.Equal(SPPrincipalType.User, actual[5].PrincipalType);
        Assert.Equal("meganb@mock.onmicrosoft.com", actual[5].UserPrincipalName);
      }
    }

    [Fact]
    public async Task EnsureUser_NullParameter_Throws()
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(
          async () => await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Web
                                  .Request()
                                  .EnsureUserAsync(null)
        );
      }
    }

    [Fact]
    public async Task EnsureUser_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/ensureuser");
      var expectedContent = "{\"logonName\":\"alexw\"}";


      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Request()
                    .EnsureUserAsync("alexw");
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

    [Fact]
    public async Task EnsureUser_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("EnsureUserResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };


      using (responseMessage)
      using (var gsc = GraphServiceTestClient.Create(responseMessage))
      {
        // ACT
        var response = await gsc.GraphServiceClient
                        .SharePointAPI(mockWebUrl)
                        .Web
                        .Request()
                        .EnsureUserAsync("alexw");
        var actual = response;

        // ASSERT
        Assert.Equal(14, actual.Id);
        Assert.False(actual.IsSiteAdmin);
        Assert.Equal("Alex Wilber", actual.Title);
        Assert.Equal(SPPrincipalType.User, actual.PrincipalType);
        Assert.Equal("alexw@mock.onmicrosoft.com", actual.UserPrincipalName);
      }
    }

#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
#pragma warning restore CA1707
  }
}
