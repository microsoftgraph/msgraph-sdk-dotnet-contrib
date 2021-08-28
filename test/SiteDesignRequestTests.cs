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
			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

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

		[Fact]
		public async Task GetAll_GeneratesCorrectRequest()
		{
			// ARRANGE
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns");

			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

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

		[Fact]
		public async Task GetById_GeneratesCorrectRequest()
		{
			// ARRANGE
			var mockSiteDesignId = Guid.NewGuid();
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesignMetadata");
			var expectedContent = $"{{\"id\":\"{mockSiteDesignId}\"}}";

			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);
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

		[Fact]
		public async Task Apply_GeneratesCorrectRequest()
		{
			// ARRANGE
			var mockRequestData = new ApplySiteDesignRequest
			{
				SiteDesignId = "mockSiteDesignId",
				WebUrl = mockWebUrl
			};
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ApplySiteDesign");
			var expectedContent = $"{{\"siteDesignId\":\"mockSiteDesignId\",\"webUrl\":\"{mockWebUrl}\"}}";

			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

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

		[Fact]
		public async Task GetAll_ReturnsCorrectResponse()
		{
			// ARRANGE
			var responseContent = ResourceManager.GetHttpResponseContent("GetSiteDesignsResponse.json");
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
		public async Task GetById_ReturnsCorrectResponse()
		{
			// ARRANGE
			var responseContent = ResourceManager.GetHttpResponseContent("GetSiteDesignMetadataResponse.json");
			HttpResponseMessage responseMessage = new HttpResponseMessage()
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(responseContent),
			};

			using (responseMessage)
			using (GraphServiceTestClient gsc = GraphServiceTestClient.Create(responseMessage))
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

		[Fact]
		public async Task Apply_ReturnsCorrectResponse()
		{
			// ARRANGE
			var mockRequestData = new ApplySiteDesignRequest
			{
				SiteDesignId = "mockSiteDesignId",
				WebUrl = mockWebUrl
			};
			var responseContent = ResourceManager.GetHttpResponseContent("ApplySiteDesignResponse.json");
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
																	.SiteDesigns
																	.Request()
																	.ApplyAsync(mockRequestData);
				var actual = response.CurrentPage;

				// ASSERT
				Assert.Equal(6, actual.Count);
				Assert.Equal(SiteScriptActionOutcome.Success, actual[1].Outcome);
				Assert.Equal("Create site column Project name", actual[1].Title);
				Assert.Null(actual[1].OutcomeText);

				Assert.Equal(SiteScriptActionOutcome.NoOp, actual[2].Outcome);
				Assert.Equal("Create content type Customer", actual[2].Title);
				Assert.Null(actual[2].OutcomeText);
			}
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public async Task GetById_MissingId_Throws(string siteDesignId)
		{
			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

			// ACT & ASSERT
			await Assert.ThrowsAsync<ArgumentNullException>(
			async () => await gsc.GraphServiceClient
															.SharePointAPI(mockWebUrl)
															.SiteDesigns[siteDesignId]
															.Request()
															.GetAsync()
			);
		}

		[Fact]
		public async Task Create_NullParams_Throws()
		{
			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);
			// ACT & ASSERT
			await Assert.ThrowsAsync<ArgumentNullException>(
			async () => await gsc.GraphServiceClient
															.SharePointAPI(mockWebUrl)
															.SiteDesigns
															.Request()
															.CreateAsync(null)
			);
		}

		[Theory]
		[InlineData("mockSiteDesign", "a0da01a9-8b93-496e-9bbd-1b53009e543e", "")]
		[InlineData("mockSiteDesign", "", "64")]
		[InlineData("", "a0da01a9-8b93-496e-9bbd-1b53009e543e", "64")]
		public async Task Create_MissingProperties_Throws(string title, string siteScriptId, string webTemplate)
		{
			var siteScriptIds = string.IsNullOrEmpty(siteScriptId)
														? null
														: new System.Collections.Generic.List<Guid>() { new Guid(siteScriptId) };

			var newSiteDesign = new SiteDesignMetadata()
			{
				Title = title,
				Description = "mockSiteDesignDescription",
				SiteScriptIds = siteScriptIds,
				WebTemplate = webTemplate,
				PreviewImageUrl = "https://mock.sharepoint.com",
				PreviewImageAltText = "mockPreviewImageAltText"
			};

			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

			await Assert.ThrowsAsync<ArgumentException>(
				async () => await gsc.GraphServiceClient
															.SharePointAPI(mockWebUrl)
															.SiteDesigns
															.Request()
															.CreateAsync(newSiteDesign)
			);
		}

		[Fact]
		public async Task Create_GeneratesCorrectRequest()
		{
			// ARRANGE
			var newSiteDesign = new SiteDesignMetadata()
			{
				Title = "mockSiteDesign",
				Description = "mockSiteDesignDescription",
				SiteScriptIds = new System.Collections.Generic.List<Guid>() { new Guid("a0da01a9-8b93-496e-9bbd-1b53009e543e") },
				WebTemplate = "64",
				PreviewImageUrl = "https://mock.sharepoint.com",
				PreviewImageAltText = "mockPreviewImageAltText"
			};
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteDesign");
			var expectedContent = "{\"info\":{\"Title\":\"mockSiteDesign\",\"Description\":\"mockSiteDesignDescription\",\"SiteScriptIds\":[\"a0da01a9-8b93-496e-9bbd-1b53009e543e\"],\"WebTemplate\":\"64\",\"PreviewImageUrl\":\"https://mock.sharepoint.com\",\"PreviewImageAltText\":\"mockPreviewImageAltText\"}}";

			using HttpResponseMessage response = new HttpResponseMessage();
			using GraphServiceTestClient gsc = GraphServiceTestClient.Create(response);

			// ACT
			await gsc.GraphServiceClient
									.SharePointAPI(mockWebUrl)
									.SiteDesigns
									.Request()
									.CreateAsync(newSiteDesign);
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
		public async Task CreateSiteDesign_ReturnsCorrectResponse()
		{
			// ARRANGE
			var newSiteDesign = new SiteDesignMetadata()
			{
				Title = "mockSiteDesign",
				Description = "mockSiteDesignDescription",
				SiteScriptIds = new System.Collections.Generic.List<Guid>() { new Guid("a0da01a9-8b93-496e-9bbd-1b53009e543e") },
				WebTemplate = "64",
				PreviewImageUrl = "https://mock.sharepoint.com",
				PreviewImageAltText = "mockPreviewImageAltText"
			};
			var responseContent = ResourceManager.GetHttpResponseContent("CreateSiteDesignResponse.json");
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
																	.SiteDesigns
																	.Request()
																	.CreateAsync(newSiteDesign);
				var actual = response;

				// ASSERT
				Assert.Equal("d3e90db2-9a67-4d24-b59f-7c54388a9cfa", actual.Id);
				Assert.Equal("mockSiteDesign", actual.Title);
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
