using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        var testHub = actual[0];
        Assert.IsType<Graph.Community.Hub>(testHub);
        Assert.Null(testHub.Description);
        Assert.False(testHub.HideNameInNavigation);
        Assert.Equal("82e8cb67-c4f1-4b38-80aa-0342294f5ece", testHub.Id);
        Assert.Equal("https://contoso.sharepoint.com/sites/ContosoTravelMarketing/SiteAssets/__hubLogo____hubLogo__.png", testHub.LogoUrl);
        Assert.Equal("205ef8f5-25bb-4547-827b-6bedb219eca2", testHub.ParentHubSiteId);
        Assert.False(testHub.RequiresJoinApproval);
        Assert.Equal(Guid.Empty.ToString(), testHub.SiteDesignId);
        Assert.Equal("82e8cb67-c4f1-4b38-80aa-0342294f5ece", testHub.SiteId);
        Assert.Equal("https://contoso.sharepoint.com/sites/ContosoTravelMarketing", testHub.SiteUrl);
        Assert.Null(testHub.Targets);
        Assert.Equal("55d9bf03-671c-45c1-8f60-c6ed9e441468", testHub.TenantInstanceId);
        Assert.Equal("Contoso Travel Marketing", testHub.Title);
      }
    }

  }
}
