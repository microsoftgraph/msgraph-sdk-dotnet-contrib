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
	[Collection("GraphService collection")]
	public class ChangeLogRequestTests
	{
		private readonly GraphServiceFixture fixture;
		private readonly ITestOutputHelper output;

		private readonly Uri mockWebUrl = new Uri("https://mock.sharepoint.com/sites/mockSite");

		public ChangeLogRequestTests(GraphServiceFixture fixture, ITestOutputHelper output)
		{
			this.fixture = fixture;
			this.output = output;
		}

		[Fact]
		public async Task GetChanges_ReturnsCorrectDerivedClasses()
		{
			// ARRANGE
			var responseContent = ResourceManager.GetHttpResponseContent("SiteGetChangesResponse.json");
			var responseMessage = new HttpResponseMessage()
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(responseContent),
			};
			var query = new ChangeQuery()
			{
				Add = true
			};

			var mockAuthProvider = new MockAuthenticationProvider();
			var mockHttpProvider = new MockHttpProvider(responseMessage, new Serializer());
			var graphServiceClient = new GraphServiceClient(mockAuthProvider.Object, mockHttpProvider.Object);

			var response = await graphServiceClient
										.SharePointAPI(mockWebUrl)
										.Site
										.Request()
										.GetChangesAsync(query);
			var actual = response.CurrentPage;

			var actualSite = actual[0] as ChangeSite;

			// ASSERT
			Assert.Equal(4, actual.Count);
			Assert.IsType<ChangeSite>(actual[0]);
			Assert.IsType<ChangeUser>(actual[1]);
			Assert.IsType<ChangeItem>(actual[2]);
			Assert.IsType<ChangeWeb>(actual[3]);
		}
	}
}
