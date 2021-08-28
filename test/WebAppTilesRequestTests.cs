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
  public class WebAppTilesRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public WebAppTilesRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task GetAll_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/apptiles");

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .AppTiles
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
    public async Task GetAll_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetWebAppTilesResponse.json");
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
                        .AppTiles
                        .Request()
                        .GetAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.Equal(10, actual.Count);
        Assert.Equal("264ae551-d91e-4a00-89c3-5d2300006cee", actual[1].AppId.ToString());
        Assert.Equal(AppStatus.Installed, actual[1].AppStatus);
        Assert.Equal("1/16/2020 10:15 PM", actual[1].LastModified);
        Assert.Equal(new DateTimeOffset(2020, 01, 16, 22, 15, 12, new TimeSpan(0)), actual[1].LastModifiedDate);
      }
    }
  }
}
