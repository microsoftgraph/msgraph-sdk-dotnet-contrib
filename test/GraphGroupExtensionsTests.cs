using Moq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Graph.Community.Test
{
  public class GraphGroupExtensionsTests
  {
    [Fact]
    public async Task AddMember_GeneratesCorrectRequest()
    {
      // ARRANGE
      var groupId = "c75bf880-6ecd-4961-9dfa-e624bfc81e22";
      var userId = "7033db53-8c04-4161-9f71-f669b10f5296";

      var expectedUri = new Uri($"https://graph.microsoft.com/v1.0/groups/{groupId}/members/$ref");
      var expectedContent = $"{{\"@odata.id\":\"https://graph.microsoft.com/v1.0/directoryObjects/{userId}\"}}";

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .Groups[groupId]
                  .Members
                  .Request()
                  .AddAsync(userId);

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

    [Fact]
    public async Task RemoveMember_GeneratesCorrectRequest()
    {
      // ARRANGE
      var groupId = "c75bf880-6ecd-4961-9dfa-e624bfc81e22";
      var userId = "7033db53-8c04-4161-9f71-f669b10f5296";

      var expectedUri = new Uri($"https://graph.microsoft.com/v1.0/groups/{groupId}/members/{userId}/$ref");

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .Groups[groupId]
                  .Members
                  .Request()
                  .RemoveAsync(userId);

      var actualContent = gsc.HttpProvider.ContentAsString;

      // ASSERT
      gsc.HttpProvider.Verify(
        provider => provider.SendAsync(
          It.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Delete &&
            req.RequestUri == expectedUri
          ),
          It.IsAny<HttpCompletionOption>(),
          It.IsAny<CancellationToken>()
          ),
        Times.Exactly(1)
      );
    }

    [Fact]
    public async Task AddOwner_GeneratesCorrectRequest()
    {
      // ARRANGE
      var groupId = "c75bf880-6ecd-4961-9dfa-e624bfc81e22";
      var userId = "7033db53-8c04-4161-9f71-f669b10f5296";

      var expectedUri = new Uri($"https://graph.microsoft.com/v1.0/groups/{groupId}/owners/$ref");
      var expectedContent = $"{{\"@odata.id\":\"https://graph.microsoft.com/v1.0/directoryObjects/{userId}\"}}";

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .Groups[groupId]
                  .Owners
                  .Request()
                  .AddAsync(userId);

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

    [Fact]
    public async Task RemoveOwner_GeneratesCorrectRequest()
    {
      // ARRANGE
      var groupId = "c75bf880-6ecd-4961-9dfa-e624bfc81e22";
      var userId = "7033db53-8c04-4161-9f71-f669b10f5296";

      var expectedUri = new Uri($"https://graph.microsoft.com/v1.0/groups/{groupId}/owners/{userId}/$ref");

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .Groups[groupId]
                  .Owners
                  .Request()
                  .RemoveAsync(userId);

      var actualContent = gsc.HttpProvider.ContentAsString;

      // ASSERT
      gsc.HttpProvider.Verify(
        provider => provider.SendAsync(
          It.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Delete &&
            req.RequestUri == expectedUri
          ),
          It.IsAny<HttpCompletionOption>(),
          It.IsAny<CancellationToken>()
          ),
        Times.Exactly(1)
      );
    }

  }
}
