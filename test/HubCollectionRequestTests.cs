using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class HubCollectionRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public HubCollectionRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void Get_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/HubSites");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient testClient = TestGraphServiceClient.Create(response);

      // ACT
      var request = testClient.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .Hubs
                          .Request()
                          .GetAsync();

      // ASSERT
      testClient.HttpProvider.Verify(
        provider => provider.SendAsync(
          It.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Get &&
            req.RequestUri == expectedUri
          ),
          It.IsAny<HttpCompletionOption>(),
          It.IsAny<CancellationToken>()
          ),
        Times.Exactly(1)
      );
    }

    [Fact]
    public async Task Get_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetHubsResponse.json");
      var responseMessage = new HttpResponseMessage()
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
                                  .Hubs
                                  .Request()
                                  .GetAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.IsAssignableFrom<IList<Hub>>(actual);
        Assert.Equal(2, actual.Count);

        var testHub = actual[1];
        Assert.IsType<Graph.Community.Hub>(testHub);
        Assert.Equal("d8ab01e9-1634-44dd-91fe-1e6ff8385c34", testHub.Id);
        Assert.Equal("Contoso Department Hub", testHub.Title);
        Assert.Null(testHub.Description);
        Assert.Equal("https://contoso.sharepoint.com/sites/ContosoDepartment/SiteAssets/__sitelogo__keep_calm_hit_refresh.png", testHub.LogoUrl);
        Assert.Equal("https://contoso.sharepoint.com/sites/ContosoDepartment", testHub.SiteUrl);
        Assert.Equal("82e8cb67-c4f1-4b38-80aa-0342294f5ece", testHub.SiteId);
        Assert.Null(testHub.Targets);
        Assert.Equal(Guid.Empty.ToString(), testHub.TenantInstanceId);
        Assert.True(testHub.RequiresJoinApproval);
      }
    }

  }
}
