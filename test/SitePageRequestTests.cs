using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class SitePageRequestTests
  {
    private readonly ITestOutputHelper output;
    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public SitePageRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GetByName_GeneratesCorrectRequestUriAndHeaders()
    {
      // ARRANGE
      var mockPagename = "champions.aspx";
      var expectedQSParms = "ListItemAllFields/ClientSideApplicationId,ListItemAllFields/PageLayoutType,ListItemAllFields/CommentsDisabled";
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/getfilebyserverrelativeurl('/sites/mockSite/SitePages/{mockPagename}')?$expand={WebUtility.UrlEncode(expectedQSParms)}");

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient testClient = GraphServiceTestClient.Create(response);

      // ACT
      var request = testClient.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .SitePages[mockPagename]
                          .Request()
                          .GetHttpRequestMessage();

      // ASSERT

      Assert.Equal(expectedUri, request.RequestUri);
      Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
      Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
      Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task GetByName_MissingId_Throws(string mockPagename)
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SitePages[mockPagename]
                                .Request()
                                .GetAsync()
        );
      }
    }

    [Fact]
    public async Task GetByName_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockPagename = "champions.aspx";

      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePageResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };

      using (responseMessage)
      using (GraphServiceTestClient gsc = GraphServiceTestClient.Create(responseMessage))
      {
        // ACT
        var testPageFileInfo = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .SitePages[mockPagename]
                                  .Request()
                                  .GetAsync();

        // ASSERT
        Assert.IsType<SitePageFileInfo>(testPageFileInfo);

        // Props checked in SitePageFileInfoConverterTests
      }
    }
  }
}
