using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
	public class TestingHandlerTests
	{
		private readonly ITestOutputHelper output;

		private readonly GraphServiceClient GraphServiceClient;
		private readonly HttpProvider HttpProvider;

		public TestingHandlerTests(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public async Task ManualMap_ThrowsIfUrlMethodNotMatched()
		{
			// ARRANGE
			var urlMatch = "/me";
			var method1 = HttpMethod.Get;
			var status1 = HttpStatusCode.InternalServerError;

			var handler = TestingHandlerBuilder.Create()
											.AddResponseMapping(urlMatch, method1, status1)
											.Build();

			using (var client = new HttpClient(handler, true))
			{
				// ACT
				// ASSERT
				await Assert.ThrowsAsync<Exception>(
					async () => await client.PatchAsync("https://graph.microsoft.com/v1.0/me", new StringContent(""))
				);

			}
		}

		[Fact]
		public async Task ManualMap_ReturnsCorrectStatus()
		{
			// ARRANGE
			var urlMatch = "/me";
			var method1 = HttpMethod.Get;
			var status1 = HttpStatusCode.InternalServerError;
			var method2 = HttpMethod.Patch;
			var status2 = HttpStatusCode.Accepted;

			var handler = TestingHandlerBuilder.Create()
											.AddResponseMapping(urlMatch, method1, status1)
											.AddResponseMapping(urlMatch, method2, status2)
											.Build();

			using (var client = new HttpClient(handler, true))
			{
				// ACT
				var getResponse = await client.GetAsync("https://graph.microsoft.com/v1.0/me");
				var patchResponse = await client.PatchAsync("https://graph.microsoft.com//me", new StringContent(""));

				// ASSERT
				Assert.Equal(status1, getResponse.StatusCode);
				Assert.Equal(status2, patchResponse.StatusCode);
			}
		}

	}
}
