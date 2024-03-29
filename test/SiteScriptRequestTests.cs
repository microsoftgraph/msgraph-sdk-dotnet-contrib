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
  public class SiteScriptRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public SiteScriptRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      // ARRANGE

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      var request = gsc.GraphServiceClient
                          .SharePointAPI(mockWebUrl)
                          .SiteScripts
                          .Request()
                          .GetHttpRequestMessage();

      // ASSERT
      Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
      Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
      Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
    }

    [Fact]
    public async Task GetAll_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScripts");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .SiteScripts
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

    [Fact]
    public async Task GetWithId_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScriptMetadata");
      var expectedContent = $"{{\"id\":\"{mockSiteScriptId}\"}}";

      using var response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      _ = await gsc.GraphServiceClient
                      .SharePointAPI(mockWebUrl)
                      .SiteScripts[mockSiteScriptId.ToString()]
                      .Request()
                      .GetAsync().ConfigureAwait(false);
      var actualContent = gsc.HttpProvider.ContentAsString;

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

      Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, gsc.HttpProvider.ContentHeaders.ContentType.MediaType);
      Assert.Equal(expectedContent, actualContent);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_MissingId_Throws(string siteScriptId)
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = TestGraphServiceClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteScripts[siteScriptId]
                                .Request()
                                .GetAsync()
        );
      }
    }
    [Fact]
    public async Task Create_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptRequest = CreateMockSiteScriptMetadata();
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteScript(Title=@title,Description=@description)?@title='{mockSiteScriptRequest.Title}'&@description='{mockSiteScriptRequest.Description}'");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      _ = await gsc.GraphServiceClient
                      .SharePointAPI(mockWebUrl)
                      .SiteScripts
                      .Request()
                      .CreateAsync(mockSiteScriptRequest);
      var actualContent = gsc.HttpProvider.ContentAsString;

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

      Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, gsc.HttpProvider.ContentHeaders.ContentType.MediaType);
      Assert.Equal(mockSiteScriptRequest.Content, actualContent);
    }

    [Fact]
    public async Task Create_NullParams_Throws()
    {
      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT & ASSERT
      await Assert.ThrowsAsync<ArgumentNullException>(
      async () => await gsc.GraphServiceClient
                              .SharePointAPI(mockWebUrl)
                              .SiteScripts
                              .Request()
                              .CreateAsync(null)
      );
    }

    [Fact]
    public async Task Create_NoTitle_Throws()
    {
      // ARRANGE
      var mockSiteScriptRequest = CreateMockSiteScriptMetadata();
      mockSiteScriptRequest.Title = string.Empty;

      using var response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT & ASSERT
      await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
      async () => await gsc.GraphServiceClient
                              .SharePointAPI(mockWebUrl)
                              .SiteScripts
                              .Request()
                              .CreateAsync(mockSiteScriptRequest)
      );
    }

    [Fact]
    public async Task Create_NoDescription_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptRequest = CreateMockSiteScriptMetadata();
      mockSiteScriptRequest.Description = string.Empty;
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteScript(Title=@title,Description=@description)?@title='{mockSiteScriptRequest.Title}'&@description='{mockSiteScriptRequest.Description}'");

      using var response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      _ = await gsc.GraphServiceClient
                      .SharePointAPI(mockWebUrl)
                      .SiteScripts
                      .Request()
                      .CreateAsync(mockSiteScriptRequest);
      var actualContent = gsc.HttpProvider.ContentAsString;

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

      Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, gsc.HttpProvider.ContentHeaders.ContentType.MediaType);
      Assert.Equal(mockSiteScriptRequest.Content, actualContent);
    }

    [Fact]
    public async Task GetAll_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetSiteScriptsResponse.json");
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
                                  .SiteScripts
                                  .Request()
                                  .GetAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.Equal(2, actual.Count);
        Assert.Equal("0d7cf729-42e7-411b-86c6-b0181f912dd4", actual[0].Id);
        Assert.Equal("mockSiteScriptTitle", actual[0].Title);
        Assert.Equal(1, actual[0].Version);
      }
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GetById_MissingId_Throws(string siteScriptId)
    {
      using var response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT & ASSERT
      await Assert.ThrowsAsync<ArgumentNullException>(
      async () => await gsc.GraphServiceClient
                              .SharePointAPI(mockWebUrl)
                              .SiteScripts[siteScriptId]
                              .Request()
                              .GetAsync()
      );
    }

    [Fact]
    public async Task GetById_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockSiteScriptId = "0d7cf729-42e7-411b-86c6-b0181f912dd4";

      var responseContent = ResourceManager.GetHttpResponseContent("GetSiteScriptMetadataResponse.json");
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
                                  .SiteScripts[mockSiteScriptId]
                                  .Request()
                                  .GetAsync();

        // ASSERT
        Assert.Equal("0d7cf729-42e7-411b-86c6-b0181f912dd4", actual.Id);
        Assert.Equal("mockSiteScriptTitle", actual.Title);
        Assert.Equal("mockSiteScriptDescription", actual.Description);
        Assert.False(actual.IsSiteScriptPackage);
        Assert.Equal(1, actual.Version);
        Assert.Equal("{\"$schema\": \"schema.json\",\"actions\": [{\"verb\": \"applyTheme\",\"themeName\": \"Red\"}],\"bindata\": { },\"version\": 1}", actual.Content);
      }
    }
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Update_MissingId_Throws(string siteScriptId)
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = TestGraphServiceClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteScripts[siteScriptId]
                                .Request()
                                .UpdateAsync(null)
        );
      }
    }

    [Fact]
    public async Task Update_MissingData_Throws()
    {
      // ARRANGE
      var mockSiteScriptId = "mockSiteScriptId";

      using (var response = new HttpResponseMessage())
      using (var gsc = TestGraphServiceClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteScripts[mockSiteScriptId]
                                .Request()
                                .UpdateAsync(null)
        );
      }
    }
    [Fact]
    public async Task Update_DifferingId_Throws()
    {
      // ARRANGE
      var mockSiteScriptId = "mockSiteScriptId";
      var mockSiteScriptMetadata = new SiteScriptMetadata()
      {
        Id = "differingId"
      };

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);
      // ACT & ASSERT
      await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
      async () => await gsc.GraphServiceClient
                              .SharePointAPI(mockWebUrl)
                              .SiteScripts[mockSiteScriptId]
                              .Request()
                              .UpdateAsync(mockSiteScriptMetadata)
      );
    }


    public static IEnumerable<object[]> GetUpdateRequests()
    {
      yield return new object[]
      {
        new SiteScriptMetadata()
        {
          Title = "UPDATED mockSiteScriptTitle"
        },
        "{\"updateInfo\":{\"Id\":\"mockSiteScriptId\",\"Title\":\"UPDATED mockSiteScriptTitle\",\"Version\":0}}"
      };
      yield return new object[]
      {
        new SiteScriptMetadata()
        {
          Description = "UPDATED mockSiteScriptDescription"
        },
        "{\"updateInfo\":{\"Id\":\"mockSiteScriptId\",\"Description\":\"UPDATED mockSiteScriptDescription\",\"Version\":0}}"
      };
      yield return new object[]
      {
        new SiteScriptMetadata()
        {
          Content = "UPDATED mockSiteScriptContent",
        },
        "{\"updateInfo\":{\"Id\":\"mockSiteScriptId\",\"Version\":0,\"Content\":\"UPDATED mockSiteScriptContent\"}}"
      };
    }

    [Theory]
    [MemberData(nameof(GetUpdateRequests))]
    public async Task EmptyProperties_SerializesCorrectly(SiteScriptMetadata updateRequest, string expectedContent)
    {
      // ARRANGE
      var mockSiteDesignId = "mockSiteScriptId";

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      //var actual = ser.SerializeObject(updateRequest);
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .SiteScripts[mockSiteDesignId]
                  .Request()
                  .UpdateAsync(updateRequest);
      var actualContent = gsc.HttpProvider.ContentAsString;

      // ASSERT
      Assert.Equal(expectedContent, actualContent);
    }

    [Fact]
    public async Task Update_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptId = "mockSiteScriptId";
      var updateSiteScript = new SiteScriptMetadata()
      {
        Title = "UPDATED mockSiteScriptTitle",
      };
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteScript");
      var expectedContent = "{\"updateInfo\":{\"Id\":\"mockSiteScriptId\",\"Title\":\"UPDATED mockSiteScriptTitle\",\"Version\":0}}";

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .SiteScripts[mockSiteScriptId]
                  .Request()
                  .UpdateAsync(updateSiteScript);
      var actualContent = gsc.HttpProvider.ContentAsString;

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

      Assert.Equal(expectedContent, actualContent);
    }


    [Fact]
    public async Task Update_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockSiteScriptId = "mockSiteScriptId";
      var updatedSiteScript = new SiteScriptMetadata()
      {
        Title = "UPDATED mockSiteScriptTitle",
        Description = "mockSiteScriptDescription"
      };
      var responseContent = ResourceManager.GetHttpResponseContent("UpdateSiteScriptResponse.json");
      HttpResponseMessage responseMessage = new HttpResponseMessage()
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
                                  .SiteScripts[mockSiteScriptId]
                                  .Request()
                                  .UpdateAsync(updatedSiteScript);
        var actual = response;

        // ASSERT
        Assert.Equal("0d7cf729-42e7-411b-86c6-b0181f912dd4", actual.Id);
        Assert.Equal("UPDATED mockSiteScriptTitle", actual.Title);
        Assert.Equal("mockSiteScriptDescription", actual.Description);
        Assert.True(actual.IsSiteScriptPackage);
      }
    }

    [Fact]
    public async Task Delete_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptId = "mockSiteScriptId";

      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.DeleteSiteScript");
      var expectedContent = $"{{\"id\":\"{mockSiteScriptId}\"}}";

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .SiteScripts[mockSiteScriptId]
                  .Request()
                  .DeleteAsync();
      var actualContent = gsc.HttpProvider.ContentAsString;

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
      Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, gsc.HttpProvider.ContentHeaders.ContentType.MediaType);
      Assert.Equal(expectedContent, actualContent);
    }

    [Fact]
    public async Task Delete_MissingId_Throws()
    {
      // ARRANGE
      string siteScriptId = default;

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      await Assert.ThrowsAsync<ArgumentNullException>(
          async () => await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .SiteDesigns[siteScriptId]
                                  .Request()
                                  .DeleteAsync()
      );


    }


    private SiteScriptMetadata CreateMockSiteScriptMetadata()
    {
      var result = new SiteScriptMetadata()
      {
        Title = "mockSiteScriptTitle",
        Description = "mockSiteScriptDescription",
        Content = "{\"$schema\": \"schema.json\",\"actions\": [{\"verb\": \"applyTheme\",\"themeName\": \"Red\"}],\"bindata\": { },\"version\": 1}",
      };
      return result;
    }
  }
}
