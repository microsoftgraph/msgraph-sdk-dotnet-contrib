using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using Moq;
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
	[Collection("GraphService collection")]
	public class SiteRequestTests
	{
		private readonly GraphServiceFixture fixture;
		private readonly ITestOutputHelper output;

		private readonly Uri mockWebUrl = new Uri("https://mock.sharepoint.com/sites/mockSite");


		public SiteRequestTests(GraphServiceFixture fixture, ITestOutputHelper output)
		{
			this.fixture = fixture;
			this.output = output;
		}

		[Fact]
		public void SiteRequest_GeneratesCorrectRequestHeaders()
		{
			// ARRANGE

			// ACT
			var request = fixture.GraphServiceClient
											.SharePointAPI(mockWebUrl)
											.Site
											.Request()
											.GetHttpRequestMessage();

			// ASSERT
			Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
			Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
			Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
		}

		[Fact]
		public async Task SiteRequest_Get_GeneratesCorrectRequest()
		{
			// ARRANGE
			var expectedUri = new Uri($"{mockWebUrl}/_api/site");

			// ACT
			await fixture.GraphServiceClient
							.SharePointAPI(mockWebUrl)
							.Site
							.Request()
							.GetAsync();

			// ASSERT
			fixture.MockHttpProvider.Verify(
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
		public async Task SiteRequest_GetChanges_GeneratesCorrectRequest()
		{
			// ARRANGE
			var query = new ChangeQuery()
			{
				Add = true
			};
			var expectedUri = new Uri($"{mockWebUrl}/_api/site/GetChanges");
			var expectedContent = "{\"query\":{\"Add\":true}}";

			// ACT
			await fixture.GraphServiceClient
							.SharePointAPI(mockWebUrl)
							.Site
							.Request()
							.GetChangesAsync(query);
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
	}
}