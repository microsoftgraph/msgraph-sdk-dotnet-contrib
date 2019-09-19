using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
	public class TestingHandlerBuilderTests
	{
		private readonly ITestOutputHelper output;

		public TestingHandlerBuilderTests(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public void CreateWithoutParameters()
		{
			var handler = TestingHandlerBuilder.Create();

			Assert.NotNull(handler);
		}

		[Fact]
		public void AddResponseMapping()
		{
			// ARRANGE
			var urlMatch = "/me";
			var method1 = HttpMethod.Get;
			var status1 = HttpStatusCode.InternalServerError;
			var method2 = HttpMethod.Patch;
			var status2 = HttpStatusCode.Accepted;

			// ACT
			var handler = TestingHandlerBuilder.Create()
											.AddResponseMapping(urlMatch, method1, status1)
											.AddResponseMapping(urlMatch, method2, status2)
											.Build();


			
			// ASSERT
#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
			Assert.Equal(1, handler.manualMap.Count);
			Assert.Equal(2, handler.manualMap[urlMatch].Count);
#pragma warning restore xUnit2013 // Do not use equality check to check for collection size.
		}
	}
}
