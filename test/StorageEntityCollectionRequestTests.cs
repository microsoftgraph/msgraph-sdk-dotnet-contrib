using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using System.Net;

namespace Graph.Community.Test
{
  public class StorageEntityCollectionRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public StorageEntityCollectionRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }


    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = TestGraphServiceClient.Create(response))
      {
        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .Tenant
                            .StorageEntities
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
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/AllProperties?$select=storageentitiesindex");

      using (var response = new HttpResponseMessage())
      using (var gsc = TestGraphServiceClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Tenant
                    .StorageEntities
                    .Request()
                    .GetAsync();

        // ASSERT
        gsc.HttpProvider.Verify(
            provider => provider.SendAsync(
                It.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri == expectedUri &&
                    req.Headers.Authorization != null
                ),
                It.IsAny<HttpCompletionOption>(),
                It.IsAny<CancellationToken>()
                ),
            Times.Exactly(1)
        );
      }
    }

    [Fact]
    public async Task Get_ReturnsCorrectResponse()
    {
      // ARRANGE
      string expectedKey = "key-2";
      var expectedEntity = new StorageEntity
      {
        Value = "random_string_value",
        Comment = "\"Added\"",
        Description = string.Empty
      };

      var responseContent = ResourceManager.GetHttpResponseContent("GetStorageEntitiesResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };

      using (responseMessage)
      using (var gsc = TestGraphServiceClient.Create(responseMessage))
      {
        // ACT
        var actual = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Tenant
                                  .StorageEntities
                                  .Request()
                                  .GetAsync();

        // ASSERT
        Assert.Equal(3, actual.Count);
        Assert.Equal(expectedEntity.Value, actual[expectedKey].Value);
        Assert.Equal(expectedEntity.Comment, actual[expectedKey].Comment);
        Assert.Equal(expectedEntity.Description, actual[expectedKey].Description);
      }
    }


  }
}
