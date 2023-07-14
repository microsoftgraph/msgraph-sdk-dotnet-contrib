using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class StorageEntityRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public StorageEntityRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task Get_GeneratesCorrectRequest()
    {
      // ARRANGE
      var entityKey = "key-2";
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/AllProperties?$select=storageentitiesindex");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .Tenant
                  .StorageEntities[entityKey]
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
      var entityKey = "key-1";
      var expected = new StorageEntity
      {
        Value = "false",
        Comment = "refer to documentation.",
        Description = "Storage entity key 1."
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
                        .StorageEntities[entityKey]
                        .Request()
                        .GetAsync();


        // ASSERT
        Assert.Equal(expected.Value, actual.Value);
        Assert.Equal(expected.Comment, actual.Comment);
        Assert.Equal(expected.Description, actual.Description);
      }
    }
  }
}
