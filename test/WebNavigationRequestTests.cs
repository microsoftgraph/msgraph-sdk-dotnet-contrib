using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Graph.Community.Test
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task

  public class WebNavigationRequestTests
  {
    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .Web
                            .Navigation
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
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/navigation");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Navigation
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
    }

    [Fact]
    public async Task Get_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = "{\"odata.metadata\":\"https://mock.sharepoint.com/_api/$metadata#SP.ApiData.Navigations/@Element\",\"odata.type\":\"SP.Navigation\",\"odata.id\":\"https://mock.sharepoint.com/_api/web/navigation\",\"odata.editLink\":\"web/navigation\",\"UseShared\":false}";
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
                                  .Navigation
                                  .Request()
                                  .GetAsync();
        var actual = response;

        // ASSERT
        Assert.False(actual.UseShared);
      }
    }

    [Fact]
    public async Task GetQuickLaunch_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/navigation/quicklaunch");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Navigation
                    .QuickLaunch
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
    }

    [Fact]
    public async Task GetTopNavigationBar_GeneratesCorrectRequest()
    {
      // ARRANGE
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/navigation/topnavigationbar");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Navigation
                    .TopNavigationBar
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
    }

    [Fact]
    public async Task GetAll_ReturnsCorrectResponse()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("NavigationNodeCollectionResponse.json");
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
                                  .Navigation
                                  .QuickLaunch
                                  .Request()
                                  .GetAsync();
        var actual = response;

        // ASSERT
        Assert.Equal(7, actual.Count);
        Assert.Equal(2003, actual[3].Id);
        Assert.True(actual[3].IsDocLib);
        Assert.False(actual[3].IsExternal);
        Assert.True(actual[3].IsVisible);
        Assert.Equal(1230, actual[3].ListTemplateType);
        Assert.Equal("Apps in Testing", actual[3].Title);
        Assert.Equal(new Uri("/Lists/DraftApps/AllItems.aspx", UriKind.Relative), actual[3].Url);
      }
    }

    [Fact]
    public async Task AddNode_NullCreationInfo_Throws()
    {
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(
          async () => await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Web
                                  .Navigation
                                  .QuickLaunch
                                  .Request()
                                  .AddAsync(null)
        );
      }
    }

    [Theory]
    [InlineData("mockTitle", "")]
    [InlineData("", "https://mocksite.com")]
    public async Task AddNode_MissingProperties_Throws(string title, string url)
    {
      var mockNewNodeRequest = new NavigationNodeCreationInformation()
      {
        Title = title,
        Url = string.IsNullOrEmpty(url) ? null : new Uri(url)
      };

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        await Assert.ThrowsAsync<ArgumentException>(
          async () => await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Web
                                  .Navigation
                                  .QuickLaunch
                                  .Request()
                                  .AddAsync(mockNewNodeRequest)
        );
      }
    }

    [Fact]
    public async Task AddNode_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockNewNodeRequest = new NavigationNodeCreationInformation()
      {
        Title = "mockTitle",
        Url = new Uri("https://mocksite.com")
      };
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/navigation/quicklaunch");
      var expectedContent = "{\"Title\":\"mockTitle\",\"Url\":\"https://mocksite.com\"}";

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Navigation
                    .QuickLaunch
                    .Request()
                    .AddAsync(mockNewNodeRequest);
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
    public async Task GetNodeById_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockNodeId = 2003;
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/navigation/getbyid({mockNodeId})");

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Navigation[mockNodeId]
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
    }

    [Fact]
    public async Task UpdateNode_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockNodeId = 2003;
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/navigation/getbyid({mockNodeId})");
      var mockUpdatedNodeRequest = new NavigationNode()
      {
        Title = "mockTitle",
        Url = new Uri("https://mocksite.com")
      };
      var expectedContent = "{\"Title\":\"mockTitle\",\"Url\":\"https://mocksite.com\"}";

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        // ACT
        await gsc.GraphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Navigation[mockNodeId]
                    .Request()
                    .UpdateAsync(mockUpdatedNodeRequest);
        var actualContent = gsc.HttpProvider.ContentAsString;

        // ASSERT
        gsc.HttpProvider.Verify(
          provider => provider.SendAsync(
            It.Is<HttpRequestMessage>(req =>
              req.Method == HttpMethod.Post &&
              req.RequestUri == expectedUri &&
              req.Headers.Authorization != null &&
              string.Join("", req.Headers.GetValues("X-HTTP-Method")) == "MERGE"
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
    public async Task UpdateNode_NullCreationInfo_Throws()
    {
      var mockNodeId = 2003;
      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(
          async () => await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Web
                                  .Navigation[mockNodeId]
                                  .Request()
                                  .UpdateAsync(null)
        );
      }
    }

    [Theory]
    [InlineData("mockTitle", "")]
    [InlineData("", "https://mocksite.com")]
    public async Task UpdateNode_MissingProperties_Throws(string title, string url)
    {
      var mockNodeId = 2003;
      var mockUpdateNodeRequest = new NavigationNode()
      {
        Title = title,
        Url = string.IsNullOrEmpty(url) ? null : new Uri(url)
      };

      using (var response = new HttpResponseMessage())
      using (var gsc = GraphServiceTestClient.Create(response))
      {
        await Assert.ThrowsAsync<ArgumentException>(
          async () => await gsc.GraphServiceClient
                                  .SharePointAPI(mockWebUrl)
                                  .Web
                                  .Navigation[mockNodeId]
                                  .Request()
                                  .UpdateAsync(mockUpdateNodeRequest)
        );
      }
    }


    [Fact]
    public void NavigationNode_Serialization_IgnoresDefaultValues()
    {
      // ARRANGE
      var mockUpdatedNodeRequest = new NavigationNode()
      {
        Title = "mockTitle",
        Url = new Uri("https://mocksite.com")
      };
      var expectedContent = "{\"Title\":\"mockTitle\",\"Url\":\"https://mocksite.com\"}";

      // ACT
      var ser = new Microsoft.Graph.Serializer();
      var actualContent = ser.SerializeObject(mockUpdatedNodeRequest);

      // ASSERT
      Assert.Equal(expectedContent, actualContent);
    }

  }

#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
#pragma warning restore CA1707 // Identifiers should not contain underscores
}
