using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Graph.Community
{
	public class TestingHandlerBuilder
	{
		internal TestingHandlerOption testingOption { get; set; }
		internal Dictionary<string, Dictionary<HttpMethod, HttpStatusCode>> responseMap;

		public TestingHandlerBuilder()
			: this(null)
		{
		}

		internal TestingHandlerBuilder(TestingHandlerOption testingOption)
		{
			this.testingOption = testingOption ?? new TestingHandlerOption();
			responseMap = new Dictionary<string, Dictionary<HttpMethod, HttpStatusCode>>();
		}

		/// <summary>
		/// Creates a <see cref="TestingHandlerBuilder"/> with default <see cref="TestingHandlerOption"/>.
		/// </summary>
		/// <param name="testingOption">Default options</param>
		/// <returns>A <see cref="TestingHandlerBuilder"/> instance with the specified defaults.</returns>
		public static TestingHandlerBuilder Create(TestingHandlerOption testingOption = null)
		{
			return new TestingHandlerBuilder(testingOption);
		}

		/// <summary>
		/// Construct the <see cref="TestingHandler"/> with the provided status or mapping
		/// </summary>
		/// <returns>A configured <see cref="TestingHandler"/></returns>
		public TestingHandler Build()
		{
			var optionMap = new Dictionary<string, Dictionary<string, int>>();

			foreach (var urlEntry in responseMap)
			{
				var methodMap = new Dictionary<string, int>();

				var urlMatch = urlEntry.Key;
				foreach (var urlMap in urlEntry.Value)
				{
					var method = urlMap.Key.Method;
					var status = (int)urlMap.Value;
					methodMap.Add(method, status);
				}

				optionMap.Add(urlEntry.Key, methodMap);
			}

			testingOption.TestingStrategy = (optionMap.Count > 0)
																			? TestingStrategy.Manual
																			: TestingStrategy.Random;
			
			return new TestingHandler(testingOption, optionMap);
		}

		/// <summary>
		/// Configures the status code and error resource that is returned from the handler
		/// </summary>
		/// <param name="statusCode">The <see cref="HttpStatusCode"/> to set on the response</param>
		/// <param name="errorResource">The <see cref="Error"/> resource to return in the response body</param>
		/// <returns></returns>
		public TestingHandlerBuilder AddResponseStatus(HttpStatusCode statusCode, Error errorResource)
		{
			testingOption.StatusCode = statusCode;
			testingOption.ErrorResource = errorResource;
			testingOption.TestingStrategy = TestingStrategy.Manual;
			return this;
		}

		/// <summary>
		/// Configures the <see cref="HttpStatusCode"/> to return form the specified <see cref="HttpMethod"/> using the specified <paramref name="urlMatch"/>
		/// </summary>
		/// <param name="urlMatch">Url or <see cref="System.Text.RegularExpressions.Regex"/> to match</param>
		/// <param name="method"><see cref="HttpMethod"/> to match</param>
		/// <param name="statusCode"><see cref="HttpStatusCode"/> to return in response</param>
		/// <returns></returns>
		public TestingHandlerBuilder AddResponseMapping(string urlMatch, HttpMethod method, HttpStatusCode statusCode)
		{
			if (responseMap.TryGetValue(urlMatch, out Dictionary<HttpMethod, HttpStatusCode> responseMapEntryForUrl))
			{
				if (responseMapEntryForUrl.ContainsKey(method))
				{
					responseMapEntryForUrl[method] = statusCode;
				}
				else
				{
					responseMapEntryForUrl.Add(method, statusCode);
				}
			}
			else
			{
				responseMapEntryForUrl = new Dictionary<HttpMethod, HttpStatusCode>();
				responseMapEntryForUrl.Add(method, statusCode);
				this.responseMap.Add(urlMatch, responseMapEntryForUrl);
			}

			return this;
		}
	}
}
