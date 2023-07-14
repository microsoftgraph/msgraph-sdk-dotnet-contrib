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
  public class ListItemRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public ListItemRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GetById_GeneratesCorrectRequestUriAndHeaders()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var mockListItemId = 1;
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/lists('{mockListId}')/items({mockListItemId})");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient testClient = TestGraphServiceClient.Create(response);

      // ACT
      var request = testClient.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .Web
                          .Lists[mockListId]
                          .Items[mockListItemId]
                          .Request()
                          .GetHttpRequestMessage();

      // ASSERT
      Assert.Equal(expectedUri, request.RequestUri);
      Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
      Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
      Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
    }

    [Fact]
    public async Task GetById_MissingId_Throws()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var mockListItemId = 0;

      using (var response = new HttpResponseMessage())
      using (var gsc = TestGraphServiceClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .Web
                                .Lists[mockListId]
                                .Items[mockListItemId]
                                .Request()
                                .GetAsync()
        );
      }
    }

    [Fact]
    public async Task GetById_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockListId = new Guid("6f094ea6-2222-4f2e-b864-54f706f8b07a");
      var mockListItemId = 1;

      var responseContent = ResourceManager.GetHttpResponseContent("GetListItemResponse.json");
      var responseMessage = new HttpResponseMessage()
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
                                  .Web
                                  .Lists[mockListId]
                                  .Items[mockListItemId]
                                  .Request()
                                  .GetAsync();

        // ASSERT
        Assert.Equal(mockListItemId, actual.Id);
        Assert.Equal("Event1", actual.Title);
        Assert.Equal("2022-05-26T17:00:00Z", actual.AdditionalData["EventDate"].ToString());
      }
    }

  }
}
