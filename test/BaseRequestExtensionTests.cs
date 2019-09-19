using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using System.Net.Http;
using Xunit;
using Graph.Community;

namespace Graph.Community.Test
{
	public class BaseRequestExtensionTests
	{
		[Fact]
		public void GraphRequestGeneratesCorrectRequestHeaders()
		{
			// ARRANGE

			using (var response = new HttpResponseMessage())
			using (var gsc = GraphServiceTestClient.Create(response))
			{
				// ACT
				var request = gsc.GraphServiceClient
														.Me
														.Request()
														.WithImmutableId()
														.GetHttpRequestMessage();

				// ASSERT
				Assert.True(request.Headers.Contains(RequestExtensionsConstants.Headers.PreferHeaderName), $"Header does not contain {RequestExtensionsConstants.Headers.PreferHeaderName} header");
				Assert.Equal(RequestExtensionsConstants.Headers.PreferHeaderImmutableIdValue, string.Join(',', request.Headers.GetValues(RequestExtensionsConstants.Headers.PreferHeaderName)));
			}
		}

		[Fact]
		public void ExtensionRequestGeneratesCorrectRequestHeaders()
		{
			// ARRANGE

			using (var response = new HttpResponseMessage())
			using (var gsc = GraphServiceTestClient.Create(response))
			{
				// ACT
				var request = gsc.GraphServiceClient
														.SharePointAPI("https://mockSite.sharepoint.com")
														.SiteScripts
														.Request()
														.WithImmutableId()
														.GetHttpRequestMessage();

				// ASSERT
				Assert.True(request.Headers.Contains(RequestExtensionsConstants.Headers.PreferHeaderName), $"Header does not contain {RequestExtensionsConstants.Headers.PreferHeaderName} header");
				Assert.Equal(RequestExtensionsConstants.Headers.PreferHeaderImmutableIdValue, string.Join(',', request.Headers.GetValues(RequestExtensionsConstants.Headers.PreferHeaderName)));
			}
		}

		[Fact]
		public void WithTestingHandlerCorrectlyRegistersAsMiddleware()
		{
			var ap = new MockAuthenticationProvider();
			var ser = new Serializer();

			var testHandler = new TestingHandler();
			var hp = new HttpProvider(testHandler, false, ser);
			var gsc = new GraphServiceClient(ap.Object, hp);

			var option = new TestingHandlerOption();

			var middlewareOptions	= gsc.Me.Request()
																.WithTestingHandler(option)
																.MiddlewareOptions;

			Assert.True(middlewareOptions.ContainsKey(typeof(TestingHandlerOption).ToString()));


		}
	}
}
