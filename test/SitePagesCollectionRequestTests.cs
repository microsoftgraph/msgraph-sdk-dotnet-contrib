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
  public class SitePagesCollectionRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public SitePagesCollectionRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void Get_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/sitepages/pages");

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient testClient = GraphServiceTestClient.Create(response);

      // ACT
      var request = testClient.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .SitePages
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
      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePagesResponse.json");
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
                                  .SitePages
                                  .Request()
                                  .GetAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.IsAssignableFrom<IList<SitePage>>(actual);
        Assert.Equal(6, actual.Count);

        var testPage = actual[1];
        Assert.IsType<Graph.Community.SitePage>(testPage);
        Assert.Equal(6, testPage.Id);
        Assert.Equal("Champions", testPage.Title);
        Assert.StartsWith("Make a difference ", testPage.Description);
        Assert.Equal(SitePagePromotedState.NotPromoted, testPage.PromotedState);
        Assert.Equal(new DateTimeOffset(2022, 5, 25, 23, 07, 42, TimeSpan.Zero), testPage.FirstPublishedDateTime);
        Assert.Equal(new DateTimeOffset(2021, 9, 10, 15, 11, 28, new TimeSpan()), testPage.LastModifiedDateTime);
        Assert.Equal("champions.aspx", testPage.FileName);
        Assert.Equal("https://mock.sharepoint.com/sites/mockSite/SitePages/champions.aspx", testPage.AbsoluteUrl);
        Assert.Equal("SitePages/champions.aspx", testPage.Url);
        Assert.Equal("cef16a53-9b15-44f2-898e-ba67b9ada101", testPage.UniqueId);
      }
    }
  }
}
