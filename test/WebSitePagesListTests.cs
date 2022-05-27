using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class WebSitePagesListTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public WebSitePagesListTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task GetSitePagesList_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetWebSitePagesListResponse.json");
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
                        .GetSitePagesListAsync();
        var actual = response;

        // ASSERT
        Assert.NotNull(actual);
        Assert.Equal("10ea14eb-9eba-4202-9def-143e9a022d85", actual.Id); 
      }
    }
  }
}
