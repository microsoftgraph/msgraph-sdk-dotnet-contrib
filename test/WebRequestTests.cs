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

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

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

    [Fact]
    public async void Get_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetWebResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };

      using (responseMessage)
      using (TestGraphServiceClient gsc = TestGraphServiceClient.Create(responseMessage))
      {
        // ACT
        var actual = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Web
                                  .Request()
                                  .Expand("RegionalSettings,RegionalSettings/TimeZone")
                                  .GetAsync();

        // ASSERT
        Assert.Equal("Mock Site", actual.Title);
        Assert.Equal("SitePages/This-one-is-not-posted.aspx", actual.WelcomePage);
        Assert.NotNull(actual.RegionalSettings);
        Assert.Equal(13, actual.RegionalSettings.TimeZone.Id);
      }
    }

    [Fact]
    public async Task Get_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

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

    [Fact]
    public void Get_GeneratesCorrectRequest_WithExpand()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web?$expand=RegionalSettings%2CRegionalSettings%2FTimeZone");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient testClient = TestGraphServiceClient.Create(response);

      // ACT
      var request = testClient.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .Web
                          .Request()
                          .Expand("RegionalSettings,RegionalSettings/TimeZone")
                          .GetHttpRequestMessage();



      // ASSERT
      Assert.Equal(expectedUri, request.RequestUri);
      Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
      Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
      Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
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

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

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

    [Fact]
    public async Task EnsureUser_NullParameter_Throws()
    {
      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .Web
                                .Request()
                                .EnsureUserAsync(null)
      );
    }

    [Fact]
    public async Task EnsureUser_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/ensureuser");
      var expectedContent = "{\"logonName\":\"alexw\"}";


      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

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

    [Fact]
    public async Task EnsureUser_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("EnsureUserResponse.json");
      HttpResponseMessage responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };


      using (responseMessage)
      using (TestGraphServiceClient gsc = TestGraphServiceClient.Create(responseMessage))
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

    [Fact]
    public async Task GetAssociatedGroups_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web?$expand=associatedownergroup%2Cassociatedmembergroup%2Cassociatedvisitorgroup&$select=id%2Ctitle%2Cassociatedownergroup%2Cassociatedmembergroup%2Cassociatedvisitorgroup");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .Request()
                  .GetAssociatedGroupsAsync();
      var actualContent = gsc.HttpProvider.ContentAsString;

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

    [Fact]
    public async Task GetAssociatedGroupsWithUsers_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web?$expand=associatedownergroup%2Cassociatedmembergroup%2Cassociatedvisitorgroup%2Cassociatedownergroup%2Fusers%2Cassociatedmembergroup%2Fusers%2Cassociatedvisitorgroup%2Fusers&$select=id%2Ctitle%2Cassociatedownergroup%2Cassociatedmembergroup%2Cassociatedvisitorgroup");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .Request()
                  .GetAssociatedGroupsAsync(true);
      var actualContent = gsc.HttpProvider.ContentAsString;

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

    [Fact]
    public async Task GetAssociatedGroups_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetWebAssociatedGroupsResponse.json");
      HttpResponseMessage responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };


      using (responseMessage)
      using (TestGraphServiceClient gsc = TestGraphServiceClient.Create(responseMessage))
      {
        // ACT
        var actual = await gsc.GraphServiceClient
                        .SharePointAPI(mockWebUrl)
                        .Web
                        .Request()
                        .GetAssociatedGroupsAsync();

        // ASSERT
        Assert.NotNull(actual.AssociatedMemberGroup);
        Assert.IsType<Group>(actual.AssociatedMemberGroup);
        Assert.NotNull(actual.AssociatedOwnerGroup);
        Assert.IsType<Group>(actual.AssociatedOwnerGroup);
        Assert.NotNull(actual.AssociatedVisitorGroup);
        Assert.IsType<Group>(actual.AssociatedVisitorGroup);
      }

    }

    [Fact]
    public async Task GetAssociatedGroupsWithUsers_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetWebAssociatedGroupsWithUsersResponse.json");
      HttpResponseMessage responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };


      using (responseMessage)
      using (TestGraphServiceClient gsc = TestGraphServiceClient.Create(responseMessage))
      {
        // ACT
        var actual = await gsc.GraphServiceClient
                        .SharePointAPI(mockWebUrl)
                        .Web
                        .Request()
                        .GetAssociatedGroupsAsync(true);

        // ASSERT
        Assert.NotNull(actual.AssociatedMemberGroup);
        Assert.IsType<Group>(actual.AssociatedMemberGroup);
        Assert.Equal(2, actual.AssociatedMemberGroup.Users.Count);
        Assert.NotNull(actual.AssociatedOwnerGroup);
        Assert.IsType<Group>(actual.AssociatedOwnerGroup);
        Assert.Single(actual.AssociatedOwnerGroup.Users);
        Assert.NotNull(actual.AssociatedVisitorGroup);
        Assert.IsType<Group>(actual.AssociatedVisitorGroup);
        Assert.Empty(actual.AssociatedVisitorGroup.Users);
      }
    }


    [Fact]
    public async Task EnsureSiteAssets_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/Lists/EnsureSiteAssetsLibrary");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .Request()
                  .EnsureSiteAssetsAsync();

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
    }

    [Fact]
    public async Task EnsureSiteAssets_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("EnsureSiteAssetsResponse.json");
      HttpResponseMessage responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };


      using (responseMessage)
      using (var gsc = TestGraphServiceClient.Create(responseMessage))
      {
        // ACT
        var response = await gsc.GraphServiceClient
                        .SharePointAPI(mockWebUrl)
                        .Web
                        .Request()
                        .EnsureSiteAssetsAsync();
        var actual = response;

        // ASSERT
        Assert.NotNull(actual);
        Assert.Equal("481f59e9-e3a8-4216-8188-642076ab4c74", actual.Id);
      }
    }

  }
}
