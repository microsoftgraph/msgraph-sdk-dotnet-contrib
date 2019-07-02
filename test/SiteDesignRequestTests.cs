using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
#pragma warning disable CA1707 //Identifiers should not contain underscores
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task

	[Collection("GraphService collection")]

	public class SiteDesignRequestTests
	{
		private readonly GraphServiceFixture fixture;
		private readonly ITestOutputHelper output;

		private readonly Uri mockWebUrl = new Uri("https://mock.sharepoint.com/sites/mockSite");


		public SiteDesignRequestTests(GraphServiceFixture fixture, ITestOutputHelper output)
		{
			this.fixture = fixture;
			this.output = output;
		}

		[Fact]
		public void SiteDesignRequest_GeneratesCorrectRequestHeaders()
		{
			// ARRANGE

			// ACT
			var request = fixture.GraphServiceClient
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
		public async Task SiteDesignRequest_GetAll_GeneratesCorrectRequest()
		{
			// ARRANGE
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns");

			// ACT
			await fixture.GraphServiceClient
							.SharePointAPI(mockWebUrl)
							.SiteDesigns
							.Request()
							.GetAsync();

			// ASSERT
			fixture.MockHttpProvider.Verify(
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
		public async Task SiteDesignRequest_GetWithId_GeneratesCorrectRequest()
		{
			// ARRANGE
			var mockSiteDesignId = Guid.NewGuid();
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesignMetadata");
			var expectedContent = $"{{\"id\":\"{mockSiteDesignId.ToString()}\"}}";

			// ACT
			_ = await fixture.GraphServiceClient
							.SharePointAPI(mockWebUrl)
							.SiteDesigns[mockSiteDesignId.ToString()]
							.Request()
							.GetAsync().ConfigureAwait(false);
			var actualContent = fixture.MockHttpProvider.ContentAsString;

			// ASSERT
			fixture.MockHttpProvider.Verify(
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

			Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, fixture.MockHttpProvider.ContentHeaders.ContentType.MediaType);
			Assert.Equal(expectedContent, actualContent);
		}

		[Fact]
		public async Task SiteDesignRequest_ApplySiteDesign_GeneratesCorrectRequest()
		{
			// ARRANGE
			var mockRequestData = new ApplySiteDesignRequest
			{
				SiteDesignId = "mockSiteDesignId",
				WebUrl = mockWebUrl
			};
			var expectedUri = new Uri($"{mockWebUrl}/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ApplySiteDesign");
			var expectedContent = $"{{\"siteDesignId\":\"mockSiteDesignId\",\"webUrl\":\"{mockWebUrl.ToString()}\"}}";

			// ACT
			_ = await fixture.GraphServiceClient
							.SharePointAPI(mockWebUrl)
							.SiteDesigns
							.Request()
							.ApplyAsync(mockRequestData);
			var actualContent = fixture.MockHttpProvider.ContentAsString;

			// ASSERT
			fixture.MockHttpProvider.Verify(
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
			Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, fixture.MockHttpProvider.ContentHeaders.ContentType.MediaType);
			Assert.Equal(expectedContent, actualContent);
		}

		//[Fact]
		//public async Task SiteDesignRequest_ApplySiteDesign_GeneratesCorrectRequestContent()
		//{
		//	// ARRANGE
		//	var mockRequestData = new ApplySiteDesignRequest
		//	{
		//		SiteDesignId = "mockSiteDesignId",
		//		WebUrl = mockWebUrl.ToString()
		//	};
		//	var expectedContent = $"{{\"siteDesignId\":\"mockSiteDesignId\",\"webUrl\":\"{mockWebUrl.ToString()}\"}}";

		//	// ACT
		//	await fixture.GraphServiceClient
		//					.SharePointAPI(mockWebUrl)
		//					.SiteDesigns
		//					.Request()
		//					.ApplyAsync(mockRequestData);
		//	var actualContent = fixture.MockHttpProvider.ContentAsString;

		//	// ASSERT
		//	Assert.Equal(Microsoft.Graph.CoreConstants.MimeTypeNames.Application.Json, fixture.MockHttpProvider.ContentHeaders.ContentType.MediaType);
		//	Assert.Equal(expectedContent, actualContent);
		//}
	}

#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
#pragma warning restore CA1707
}
