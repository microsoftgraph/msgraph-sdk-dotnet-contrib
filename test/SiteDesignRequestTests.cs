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
				// ACT
				var request = gsc.GraphServiceClient
														.SharePointAPI(mockWebUrl)
														.SiteDesigns
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
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns");

			using (var response = new HttpResponseMessage())
			using (var gsc = GraphServiceTestClient.Create(response))
			{
				// ACT
				await gsc.GraphServiceClient
										.SharePointAPI(mockWebUrl)
										.SiteDesigns
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
		public async Task ApplySiteDesign_GeneratesCorrectRequest()
		{
			// ARRANGE
			var mockRequestData = new ApplySiteDesignRequest
			{
				SiteDesignId = "mockSiteDesignId",
				WebUrl = mockWebUrl
			};
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ApplySiteDesign");
			var expectedContent = $"{{\"siteDesignId\":\"mockSiteDesignId\",\"webUrl\":\"{mockWebUrl.ToString()}\"}}";

			using (var response = new HttpResponseMessage())
			using (var gsc = GraphServiceTestClient.Create(response))
			{
				// ACT
				_ = await gsc.GraphServiceClient
												.SharePointAPI(mockWebUrl)
												.SiteDesigns
												.Request()
												.ApplyAsync(mockRequestData);
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
		public async Task GetAll_ReturnsCorrectResponse()
		{
			// ARRANGE
			var responseContent = ResourceManager.GetHttpResponseContent("GetSiteDesignsResponse.json");
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
																	.SiteDesigns
																	.Request()
																	.GetAsync();
				var actual = response.CurrentPage;

				// ASSERT
				Assert.Equal(2, actual.Count);
				Assert.Equal("a9ead935-38a6-45dd-a217-4c6d79c64187", actual[0].Id);
				Assert.Equal("mockSiteDesignTitle", actual[0].Title);
				Assert.Equal(1, actual[0].Version);
				Assert.Equal("mockSiteDesignDescription", actual[0].Description);
				Assert.Equal(Guid.Empty, actual[0].DesignPackageId);
				Assert.False(actual[0].IsDefault);
				Assert.Equal("mockPreviewImageAltText", actual[0].PreviewImageAltText);
				Assert.Equal("mockPreviewImageUrl", actual[0].PreviewImageUrl);
				Assert.Equal("64", actual[0].WebTemplate);
				Assert.Single(actual[0].SiteScriptIds);
				Assert.Equal(new Guid("515473d3-f08f-4e35-b9c3-285e1bcf7cd0"), actual[0].SiteScriptIds[0]);
			}
		}

		[Fact]
		public async Task GetWithId_ReturnsCorrectResponse()
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
				var response = await gsc.GraphServiceClient
																	.SharePointAPI(mockWebUrl)
																	.SiteDesigns["52693c3f-4f86-49e1-8d93-bb18ad8d22f8"]
																	.Request()
																	.GetAsync();
				var actual = response.CurrentPage;

				// ASSERT
				Assert.Equal(1, actual.Count);
				Assert.Equal("52693c3f-4f86-49e1-8d93-bb18ad8d22f8", actual[0].Id);
				Assert.Equal("mockSiteDesignTitle", actual[0].Title);
				Assert.Equal(1, actual[0].Version);
				Assert.Equal("mockSiteDesignDescription", actual[0].Description);
				Assert.Equal(Guid.Empty, actual[0].DesignPackageId);
				Assert.False(actual[0].IsDefault);
				Assert.Equal("mockPreviewImageAltText", actual[0].PreviewImageAltText);
				Assert.Equal("mockPreviewImageUrl", actual[0].PreviewImageUrl);
				Assert.Equal("64", actual[0].WebTemplate);
				Assert.Equal(2,actual[0].SiteScriptIds.Count);
				Assert.Equal(new Guid("ebb4bcd6-1c19-47fc-9910-fb618d2d3c13"), actual[0].SiteScriptIds[1]);
			}
		}
	}

#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
#pragma warning restore CA1707
}
