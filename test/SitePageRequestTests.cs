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
      using TestGraphServiceClient testClient = TestGraphServiceClient.Create(response);

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
      using (var gsc = TestGraphServiceClient.Create(response))
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
      using (TestGraphServiceClient gsc = TestGraphServiceClient.Create(responseMessage))
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

    [Fact]
    public void GetVersions_GeneratesCorrectRequestUriAndHeaders()
    {
      // ARRANGE
      var mockPagename = "champions.aspx";
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/getfilebyserverrelativeurl('/sites/mockSite/SitePages/{mockPagename}')/versions");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient testClient = TestGraphServiceClient.Create(response);

      // ACT
      var request = testClient.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .SitePages[mockPagename]
                          .Versions
                          .Request()
                          .GetHttpRequestMessage();

      // ASSERT

      Assert.Equal(expectedUri, request.RequestUri);
      Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
      Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
      Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));

    }

    [Fact]
    public async Task GetVersions_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockPagename = "champions.aspx";

      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePageVersionsResponse.json");
      var responseMessage = new HttpResponseMessage()
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
                                    .SitePages[mockPagename]
                                    .Versions
                                    .Request()
                                    .GetAsync();


        var actual = response.CurrentPage;

        //// ASSERT
        Assert.IsAssignableFrom<IList<SitePageVersion>>(actual);
        Assert.Equal(3, actual.Count);

        var testVersion = actual[1];
        Assert.Equal(new DateTime(2023, 2, 20, 21, 33, 6), testVersion.Created);
        Assert.True(testVersion.IsCurrentVersion);
        Assert.Equal(27887, testVersion.Size);
        Assert.Equal("2.0", testVersion.VersionLabel);


      }
    }
  }
}
