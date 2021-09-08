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
  public class SiteDesignRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public SiteDesignRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ARRANGE
        var mockSiteDesignId = "mockSiteDesignId";

        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .SiteDesigns[mockSiteDesignId]
                            .Request()
                            .GetHttpRequestMessage();

        // ASSERT
        Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
        Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
        Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
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
                                .SiteDesigns[siteDesignId]
                                .Request()
                                .GetAsync()
        );
      }
    }

    [Fact]
    public async Task Get_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteDesignId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesignMetadata");
      var expectedContent = $"{{\"id\":\"{mockSiteDesignId.ToString()}\"}}";

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        _ = await gsc.GraphServiceClient
                        .SharePointAPI(mockWebUrl)
                        .SiteDesigns[mockSiteDesignId.ToString()]
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
    public async Task Get_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetSiteDesignMetadataResponse.json");
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
                                  .SiteDesigns["52693c3f-4f86-49e1-8d93-bb18ad8d22f8"]
                                  .Request()
                                  .GetAsync();

        // ASSERT
        Assert.Equal("52693c3f-4f86-49e1-8d93-bb18ad8d22f8", actual.Id);
        Assert.Equal("mockSiteDesignTitle", actual.Title);
        Assert.Equal(1, actual.Version);
        Assert.Equal("mockSiteDesignDescription", actual.Description);
        Assert.Equal(Guid.Empty, actual.DesignPackageId);
        Assert.False(actual.IsDefault);
        Assert.Equal("mockPreviewImageAltText", actual.PreviewImageAltText);
        Assert.Equal("mockPreviewImageUrl", actual.PreviewImageUrl);
        Assert.Equal("64", actual.WebTemplate);
        Assert.Equal(2, actual.SiteScriptIds.Count);
        Assert.Equal(new Guid("ebb4bcd6-1c19-47fc-9910-fb618d2d3c13"), actual.SiteScriptIds[1]);
      }
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Update_MissingId_Throws(string siteDesignId)
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteDesigns[siteDesignId]
                                .Request()
                                .UpdateAsync(null)
        );
      }
    }

    [Fact]
    public async Task Update_MissingData_Throws()
    {
      // ARRANGE
      var mockSiteDesignId = "mockSiteDesignId";

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentNullException>(
        async () => await gsc.GraphServiceClient
                                .SharePointAPI(mockWebUrl)
                                .SiteDesigns[mockSiteDesignId]
                                .Request()
                                .UpdateAsync(null)
        );
      }
    }

    [Fact]
    public async Task Update_DifferingId_Throws()
    {
      // ARRANGE
      var mockSiteDesignId = "mockSiteDesignId";
      var mockSiteDesignMetadata = new SiteDesignMetadata()
      {
        Id = "differingId"
      };

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);
      // ACT & ASSERT
      await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
      async () => await gsc.GraphServiceClient
                              .SharePointAPI(mockWebUrl)
                              .SiteDesigns[mockSiteDesignId]
                              .Request()
                              .UpdateAsync(mockSiteDesignMetadata)
      );
    }

    [Fact]
    public async Task Update_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockSiteDesignId = "mockSiteDesignId";
      var updateSiteDesign = new SiteDesignMetadata()
      {
        Title = "UPDATED mockSiteDesignTitle",
        Description = "mockSiteDesignDescription",
        SiteScriptIds = new System.Collections.Generic.List<Guid>() { new Guid("a0da01a9-8b93-496e-9bbd-1b53009e543e") },
        WebTemplate = "64",
        PreviewImageUrl = "https://mock.sharepoint.com",
        PreviewImageAltText = "mockPreviewImageAltText"
      };
      var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteDesign");
      var expectedContent = "{\"updateInfo\":{\"Id\":\"mockSiteDesignId\",\"Title\":\"UPDATED mockSiteDesignTitle\",\"Description\":\"mockSiteDesignDescription\",\"SiteScriptIds\":[\"a0da01a9-8b93-496e-9bbd-1b53009e543e\"],\"WebTemplate\":\"64\",\"PreviewImageUrl\":\"https://mock.sharepoint.com\",\"PreviewImageAltText\":\"mockPreviewImageAltText\"}}";

      using HttpResponseMessage response = new HttpResponseMessage();
      using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

      // ACT
      await gsc.GraphServiceClient
                  .SharePointAPI(mockWebUrl)
                  .SiteDesigns[mockSiteDesignId]
                  .Request()
                  .UpdateAsync(updateSiteDesign);
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
    public async Task UpdateSiteDesign_ReturnsCorrectResponse()
    {
      // ARRANGE
      var mockSiteDesignId = "mockSiteDesignId";
      var updatedSiteDesign = new SiteDesignMetadata()
      {
        Title = "UPDATED mockSiteDesign",
        Description = "mockSiteDesignDescription",
        SiteScriptIds = new System.Collections.Generic.List<Guid>() { new Guid("a0da01a9-8b93-496e-9bbd-1b53009e543e") },
        WebTemplate = "64",
        PreviewImageUrl = "https://mock.sharepoint.com",
        PreviewImageAltText = "mockPreviewImageAltText"
      };
      var responseContent = ResourceManager.GetHttpResponseContent("UpdateSiteDesignResponse.json");
      HttpResponseMessage responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };

      using (responseMessage)
      using (GraphServiceTestClient gsc = GraphServiceTestClient.Create(responseMessage))
      {
        // ACT
        var response = await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .SiteDesigns[mockSiteDesignId]
                                  .Request()
                                  .UpdateAsync(updatedSiteDesign);
        var actual = response;

        // ASSERT
        Assert.Equal("d3e90db2-9a67-4d24-b59f-7c54388a9cfa", actual.Id);
        Assert.Equal("UPDATED mockSiteDesignTitle", actual.Title);
        Assert.Equal("mockSiteDesignDescription", actual.Description);
        Assert.Null(actual.DesignPackageId);
        Assert.False(actual.IsDefault);
        Assert.Equal("mockPreviewImageAltText", actual.PreviewImageAltText);
        Assert.Equal("https://mock.sharepoint.com", actual.PreviewImageUrl);
        Assert.Equal("64", actual.WebTemplate);
        Assert.Single(actual.SiteScriptIds);
        Assert.Equal(new Guid("a0da01a9-8b93-496e-9bbd-1b53009e543e"), actual.SiteScriptIds[0]);
      }
    }

  }
}
