using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Graph.Community.Test
{
	public class GraphServiceFixture
	{

		public GraphServiceFixture()
		{
			// run once, used by all tests attributed with the Collection

			var mockAuthProvider = new MockAuthenticationProvider();
			this.MockHttpProvider = new MockHttpProvider(new HttpResponseMessage(), new Serializer());
			this.GraphServiceClient = new GraphServiceClient(mockAuthProvider.Object, this.MockHttpProvider.Object);
		}

		public void Dispose()
		{
			// run once after all attributed tests complete
		}

		public GraphServiceClient GraphServiceClient { get; private set; }

		public MockHttpProvider MockHttpProvider { get; private set; }
	}

	[CollectionDefinition("GraphService collection")]
	public class GraphServiceFixtureCollection : ICollectionFixture<GraphServiceFixture>
	{
		// This class has no code, and is never created. Its purpose is simply
		// to be the place to apply [CollectionDefinition] and all the
		// ICollectionFixture<> interfaces.
	}
}
