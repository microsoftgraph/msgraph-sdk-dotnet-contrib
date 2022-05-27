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
  public class ListItemCollectionRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public ListItemCollectionRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      // ARRANGE

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .Web
                            .Lists
                            .Request()
                            .GetHttpRequestMessage();

        // ASSERT
        Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
        Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
        Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
      }

    }

    [Fact]
    public async Task Get_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/lists('{mockListId}')/items");

      using var response = new HttpResponseMessage();
      using var gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Web
                  .Lists[mockListId]
                  .Items
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
    public async Task Get_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();

      var responseContent = ResourceManager.GetHttpResponseContent("GetListItemsResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };

      ChangeToken expectedChangeToken = new ChangeToken() { StringValue = "1;3;f12b876b-54d7-44e4-9dad-122bedd899a6;637891749137700000;350718294" };

      using (responseMessage)
      using (var gsc = GraphServiceTestClient.Create(responseMessage))
      {
        // ACT
        var response = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Web
                                  .Lists[mockListId]
                                  .Items
                                  .Request()
                                  .GetAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.Equal(2, actual.Count);
        Assert.Equal(1, actual[0].Id);
        Assert.Equal("Event2", actual[1].Title);
      }
    }

    [Fact]
    public async Task GetAsSitePageListItems_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var encodedExpand = WebUtility.UrlEncode("Author,Editor,CheckoutUser");
      var encodedSelect = WebUtility.UrlEncode("Id,Title,Description,Created,Modified,FirstPublishedDate,OData__ModernAudienceTargetUserFieldId,PromotedState,Author/Title,Author/Name,Author/EMail,Editor/Title,Editor/Name,Editor/EMail,CheckoutUser/Title,CheckoutUser/Name,CheckoutUser/EMail");
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/lists('{mockListId}')/items?$expand={encodedExpand}&$select={encodedSelect}");


      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient testClient = GraphServiceTestClient.Create(response);

      // ACT
      var request = testClient.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .Web
                          .Lists[mockListId]
                          .Items
                          .Request()
                          .GetAsSitePageListItemAsync();

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
    public async Task GetAsSitePageListItems_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();

      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePageListItemsResponse.json");
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
                                  .Lists[mockListId]
                                  .Items
                                  .Request()
                                  .GetAsSitePageListItemAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.Equal(3, actual.Count);

        Assert.IsType<Graph.Community.SitePageListItem>(actual[0]);
        Assert.Equal(1, actual[0].Id);
        Assert.IsType<Graph.Community.UserInfo>(actual[0].Author);
        Assert.Equal("SHAREPOINT\\system", actual[0].Author.Name);
        Assert.Null(actual[0].ModernAudienceTargetUsers);

        Assert.Equal("News Page", actual[1].Title);
        Assert.StartsWith("Hello!", actual[1].Description);
        Assert.Equal(new DateTime(2022, 05, 25, 23, 7, 42), actual[1].FirstPublishedDate);
        Assert.Equal("Mock User", actual[1].Editor.Title);
        Assert.Null(actual[1].CheckoutUser);

        Assert.Equal(SitePagePromotedState.PromoteOnPublish, actual[2].PromotedState);
        Assert.Equal(new DateTimeOffset(2022, 5, 25, 23, 7, 56, new TimeSpan()), actual[2].CreatedDateTime);
        Assert.Equal(new DateTimeOffset(2022, 5, 26, 12, 7, 28, new TimeSpan()), actual[2].LastModifiedDateTime);
        Assert.Equal("user@mock.microsoftonline.com", actual[2].CheckoutUser.Email);
        Assert.Equal(2, actual[2].ModernAudienceTargetUsers.Count);
      }
    }

  }
}
