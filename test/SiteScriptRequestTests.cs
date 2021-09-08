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
      var mockSiteScriptId = Guid.NewGuid();

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .SiteScripts[mockSiteScriptId.ToString()]
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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_MissingId_Throws(string siteDesignId)
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
                                .GetAsync()
        );
      }
    }

    [Fact]
    public async Task Get_ReturnsCorrectResponse()
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
        var actual = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .SiteScripts[mockSiteScriptId]
                                  .Request()
                                  .GetAsync();

        // ASSERT
        Assert.Equal("0d7cf729-42e7-411b-86c6-b0181f912dd4", actual.Id);
        Assert.Equal("mockSiteScriptTitle", actual.Title);
        Assert.Equal(1, actual.Version);
        Assert.Equal("{\"$schema\": \"schema.json\",\"actions\": [{\"verb\": \"applyTheme\",\"themeName\": \"Red\"}],\"bindata\": { },\"version\": 1}", actual.Content);
      }
    }
  }
}
