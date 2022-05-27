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
        var testPage = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .SitePages[mockPagename]
                                  .Request()
                                  .GetAsync();

        // ASSERT
        Assert.IsType<SitePageFileInfo>(testPage);

        // explicit props
        //Assert.Equal(2, actual[2].ModernAudienceTargetUsers.Count);
        //Assert.Equal(new DateTimeOffset(2022, 5, 25, 23, 7, 56, new TimeSpan()), actual[2].CreatedDateTime);
        //Assert.IsType<Graph.Community.UserInfo>(actual[0].Author);
        //Assert.Equal("Mock User", actual[1].Editor.Title);
        //Assert.Null(actual[1].CheckoutType);
        //Assert.Null(actual[1].CheckoutUser);
        //Assert.Equal("2022-05-26T17:00:00Z", testPage.ServerRelativeUrl);

        // inherited props
        Assert.Equal(6, testPage.Id);
        Assert.Equal("Champions", testPage.Title);
        Assert.StartsWith("Make a difference ", testPage.Description);
        Assert.Equal(SitePagePromotedState.NotPromoted, testPage.PromotedState);
        Assert.Null(testPage.FirstPublishedDate);
        Assert.Equal(new DateTimeOffset(2021, 9, 10, 15, 11, 28, new TimeSpan()), testPage.LastModifiedDateTime);
        Assert.Equal("champions.aspx", testPage.FileName);
        Assert.Equal("cef16a53-9b15-44f2-898e-ba67b9ada101", testPage.UniqueId);

      }
    }
  }
}
