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
#pragma warning disable CA1707 //Identifiers should not contain underscores
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task

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

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
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
    }

    [Fact]
    public async Task GetAll_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScripts");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
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
    }

    [Fact]
    public async Task GetWithId_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScriptMetadata");
      var expectedContent = $"{{\"id\":\"{mockSiteScriptId.ToString()}\"}}";

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
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
    }

    [Fact]
    public async Task Create_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptRequest = CreateMockSiteScriptMetadata();
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteScript(Title=@title,Description=@description)?@title='{mockSiteScriptRequest.Title}'&@description='{mockSiteScriptRequest.Description}'");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
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
    }

    [Fact]
    public async Task Create_NullParams_Throws()
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteScripts
                                .Request()
                                .CreateAsync(null)
        );
      }
    }

    [Fact]
    public async Task Create_NoTitle_Throws()
    {
      // ARRANGE
      var mockSiteScriptRequest = CreateMockSiteScriptMetadata();
      mockSiteScriptRequest.Title = string.Empty;

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteScripts
                                .Request()
                                .CreateAsync(mockSiteScriptRequest)
        );
      }
    }

    [Fact]
    public async Task Create_NoDescription_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteScriptRequest = CreateMockSiteScriptMetadata();
      mockSiteScriptRequest.Description = string.Empty;
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteScript(Title=@title,Description=@description)?@title='{mockSiteScriptRequest.Title}'&@description='{mockSiteScriptRequest.Description}'");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
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
      using (var gsc = GraphServiceTestClient.Create(responseMessage))
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
    public async Task GetWithId_MissingId_Throws(string siteDesignId)
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteScripts[siteDesignId]
                                .Request()
                                .CreateAsync(null)
        );
      }
    }

    [Fact]
    public async Task GetWithId_ReturnsCorrectResponse()
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
      using (var gsc = GraphServiceTestClient.Create(responseMessage))
      {
        // ACT
        var response = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .SiteScripts[mockSiteScriptId]
                                  .Request()
                                  .GetAsync();
        var actual = response.CurrentPage;

        // ASSERT
        Assert.Equal(1, actual.Count);
        Assert.Equal("0d7cf729-42e7-411b-86c6-b0181f912dd4", actual[0].Id);
        Assert.Equal("mockSiteScriptTitle", actual[0].Title);
        Assert.Equal(1, actual[0].Version);
        Assert.Equal("{\"$schema\": \"schema.json\",\"actions\": [{\"verb\": \"applyTheme\",\"themeName\": \"Red\"}],\"bindata\": { },\"version\": 1}", actual[0].Content);
      }
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

#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
#pragma warning restore CA1707
}
