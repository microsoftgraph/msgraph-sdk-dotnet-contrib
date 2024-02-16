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
  public class HubRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public HubRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task GetById_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockHubSiteId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/HubSites/GetById?hubSiteId=%27{mockHubSiteId}%27");

      using HttpResponseMessage response = new();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);
      // ACT
      _ = await gsc.GraphServiceClient
                                      .SharePointAPI(mockWebUrl)
                                      .Hubs[mockHubSiteId.ToString()]
                                      .Request()
                                      .GetAsync().ConfigureAwait(false);
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
    public async Task GetById_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetHubResponse.json");
      HttpResponseMessage responseMessage = new()
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
                            .Hubs["d8ab01e9-1634-44dd-91fe-1e6ff8385c34"]
                            .Request()
                            .GetAsync();

        // ASSERT
        Assert.IsType<Hub>(actual);
        Assert.Null(actual.Description);
        Assert.False(actual.HideNameInNavigation);
        Assert.Equal("d8ab01e9-1634-44dd-91fe-1e6ff8385c34", actual.Id);
        Assert.Equal("https://contoso.sharepoint.com/sites/ContosoDepartment/SiteAssets/__sitelogo__keep_calm_hit_refresh.png", actual.LogoUrl);
        Assert.Equal(Guid.Empty.ToString(), actual.ParentHubSiteId);
        Assert.True(actual.RequiresJoinApproval);
        Assert.Equal(Guid.Empty.ToString(), actual.SiteDesignId);
        Assert.Equal("82e8cb67-c4f1-4b38-80aa-0342294f5ece", actual.SiteId);
        Assert.Equal("https://contoso.sharepoint.com/sites/ContosoDepartment", actual.SiteUrl);
        Assert.NotNull(actual.Targets);
        Assert.Equal("55d9bf03-671c-45c1-8f60-c6ed9e441468", actual.TenantInstanceId);
        Assert.Equal("Contoso Department Hub", actual.Title);
      }
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GetById_MissingId_Throws(string hubId)
    {
      using HttpResponseMessage response = new();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT & ASSERT
      await Assert.ThrowsAsync<ArgumentNullException>(
      async () => await gsc.GraphServiceClient
                              .SharePointAPI(mockWebUrl)
                              .Hubs[hubId]
                              .Request()
                              .GetAsync()
      );
    }

  }
}
