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
  public class SearchRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public SearchRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      var request = gsc.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .Search
                          .Request()
                          .GetHttpRequestMessage();

      // ASSERT
      Assert.Equal(SharePointAPIRequestConstants.Headers.SearchAcceptHeaderValue, request.Headers.Accept.ToString());
    }

    [Fact]
    public async Task Query_GeneratesCorrectRequest()
    {
      // ARRANGE
      var searchText = "searchText";
      var expectedUri = new Uri($"{mockWebUrl}/_api/search/query?queryText={searchText}");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);
      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Search
                  .Request()
                  .QueryAsync(searchText);

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
    public async Task Query_ReturnsCorrectResponse()
    {
      // ARRANGE
      var searchText = "sharepoint";
      var responseContent = ResourceManager.GetHttpResponseContent("SearchQueryResponse.json");
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
                                  .Search
                                  .Request()
                                  .QueryAsync(searchText);
        var actual = response;

        // ASSERT
        Assert.Equal(5, actual.PrimaryQueryResult.RelevantResults.RowCount);
        Assert.Equal(3945, actual.PrimaryQueryResult.RelevantResults.TotalRows);
        Assert.Equal("Megan Bowen", actual.PrimaryQueryResult.RelevantResults.Table.Rows[2].Cells[4].Value);

      }
    }

    [Fact]
    public async Task PostQuery_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/search/postquery");
      var mockPostQueryRequest = new SearchQuery("sharepoint", new List<string> { "Title", "Author" });
      var expectedContent = "{\"request\":{\"Querytext\":\"sharepoint\",\"SelectProperties\":{\"results\":[\"Title\",\"Author\"]}}}";

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Search
                  .Request()
                  .PostQueryAsync(mockPostQueryRequest);
      var actualContent = gsc.HttpProvider.ContentAsString;

      // ASSERT
      gsc.HttpProvider.Verify(
        provider => provider.SendAsync(
          It.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Post &&
            req.RequestUri == expectedUri
          ),
          It.IsAny<HttpCompletionOption>(),
          It.IsAny<CancellationToken>()
          ),
        Times.Exactly(1)
      );
      Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, gsc.HttpProvider.ContentHeaders.ContentType.MediaType);
      Assert.Equal(expectedContent, actualContent);
    }
  }
}
