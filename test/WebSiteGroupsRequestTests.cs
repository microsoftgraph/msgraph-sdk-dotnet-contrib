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
  public class WebSiteGroupsRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";


    public WebSiteGroupsRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }


    [Fact]
    public async Task GetSiteGroups_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/sitegroups");

      using var response = new HttpResponseMessage();
      using var gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .SiteGroups
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
    public async Task GetSiteGroups_GeneratesCorrectRequest_WithExpand()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/sitegroups?$expand=Users");

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .SiteGroups
                  .Request()
                  .Expand("Users")
                  .GetAsync();

      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .SiteGroups
                  .Request()
                  .Expand(g => g.Users)
                  .GetAsync();

      // ASSERT
      gsc.HttpProvider.Verify(
        provider => provider.SendAsync(
          It.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Get &&
            req.RequestUri.ToString().ToLower() == expectedUri.ToString().ToLower() &&
            req.Headers.Authorization != null
          ),
          It.IsAny<HttpCompletionOption>(),
          It.IsAny<CancellationToken>()
        ),
        Times.Exactly(2)
      );
    }

    [Fact]
    public async Task GetSiteGroups_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetSiteGroupsResponse.json");
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
                        .SiteGroups
                        .Request()
                        .GetAsync();
        var actual = response;

        // ASSERT
        Assert.Equal(5, actual.Count);
        Assert.Equal(4, actual[2].Id);
        Assert.False(actual[2].AllowMembersEditMembership);
        Assert.Equal("Mock Site Visitors", actual[2].Title);
        Assert.Equal(SPPrincipalType.SharePointGroup, actual[2].PrincipalType);
      }
    }

    [Fact]
    public async Task GetSiteGroupsWithExpand_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetSiteGroupsExpandUsersResponse.json");
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
                        .SiteGroups
                        .Request()
                        .Expand(g => g.Users)
                        .GetAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.Equal(5, actual.Count);
        Assert.Equal(4, actual[2].Id);
        Assert.False(actual[2].AllowMembersEditMembership);
        Assert.Equal("Mock Site Visitors", actual[2].Title);
        Assert.Equal(SPPrincipalType.SharePointGroup, actual[2].PrincipalType);

        Assert.Single(actual[2].Users);

        var user = actual[2].Users[0];
        Assert.Equal(16, user.Id);
        Assert.False(user.IsSiteAdmin);
        Assert.Equal("demo1 Members", user.Title);
        Assert.Equal(SPPrincipalType.SecurityGroup, user.PrincipalType);
        Assert.Null(user.UserPrincipalName);
      }
    }
  }
}
