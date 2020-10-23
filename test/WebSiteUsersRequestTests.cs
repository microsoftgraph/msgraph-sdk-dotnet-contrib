using Microsoft.Graph;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class WebSiteUsersRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public WebSiteUsersRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task GetAll_GeneratesCorrectRequest()
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
    public async Task GetAll_ReturnsCorrectResponse()
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
      }
    }

    [Fact]
    public async Task GetById_GeneratesCorrectRequest()
    {
      // ARRANGE
      int testUserId = 10;
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/siteusers/getbyid({testUserId})");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .SiteUsers[testUserId]
                    .Request()
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
          Times.Exactly(1)
        );
      }
    }

    [Fact]
    public async Task GetById_ReturnsCorrectResponse()
    {
      // ARRANGE
      int testUserId = 10;

      var responseContent = ResourceManager.GetHttpResponseContent("GetSiteUserResponse.json");

      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };


      using (responseMessage)
      using (var gsc = GraphServiceTestClient.Create(responseMessage))
      {
        // ACT
        var actual = await gsc.GraphServiceClient
                        .SharePointAPI(mockWebUrl)
                        .Web
                        .SiteUsers[testUserId]
                        .Request()
                        .GetAsync();

        // ASSERT
        Assert.Equal(testUserId, actual.Id);
        Assert.False(actual.IsSiteAdmin);
        Assert.Equal("Megan Bowen", actual.Title);
        Assert.Equal(SPPrincipalType.User, actual.PrincipalType);
        Assert.Equal("meganb@mock.onmicrosoft.com", actual.UserPrincipalName);
      }
    }
  }
}
