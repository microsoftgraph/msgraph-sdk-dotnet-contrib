using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Graph.Community.Test
{
	public class BaseRequestExtensionTests
	{
		[Fact]
		public void GraphRequestWithImmutableIdGeneratesCorrectRequestHeaders()
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
	}
}
