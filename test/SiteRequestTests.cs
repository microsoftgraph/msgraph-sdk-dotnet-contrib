using Moq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task

	public class SiteRequestTests
	{
		private readonly ITestOutputHelper output;

		private readonly Uri mockWebUrl = new Uri("https://mock.sharepoint.com/sites/mockSite");


		public SiteRequestTests(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public void SiteRequest_GeneratesCorrectRequestHeaders()
		{
			using (var response = new HttpResponseMessage())
			using (var gsc = GraphServiceTestClient.Create(response))
			{
				// ACT
				var request = gsc.GraphServiceClient
														.SharePointAPI(mockWebUrl)
														.Site
														.Request()
														.GetHttpRequestMessage();

				// ASSERT
				Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
				Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
				Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
			}
		}

		[Fact]
		public async Task SiteRequest_Get_GeneratesCorrectRequest()
		{
			// ARRANGE
			var expectedUri = new Uri($"{mockWebUrl}/_api/site");

			using (var response = new HttpResponseMessage())
			using (var gsc = GraphServiceTestClient.Create(response))
			{
				// ACT
				await gsc.GraphServiceClient
										.SharePointAPI(mockWebUrl)
										.Site
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
		public async Task SiteRequest_GetChanges_GeneratesCorrectRequest()
		{
			// ARRANGE
			var query = new ChangeQuery()
			{
				Add = true
			};
			var expectedUri = new Uri($"{mockWebUrl}/_api/site/GetChanges");
			var expectedContent = "{\"query\":{\"Add\":true}}";

			using (var response = new HttpResponseMessage())
			using (var gsc = GraphServiceTestClient.Create(response))
			{
				// ACT
				await gsc.GraphServiceClient
										.SharePointAPI(mockWebUrl)
										.Site
										.Request()
										.GetChangesAsync(query);
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
	}

#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
#pragma warning restore CA1707
}