using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class SearchRequestBuilder : BaseRequestBuilder, ISearchRequestBuilder
	{
		private readonly IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
		public SearchRequestBuilder(
			string requestUrl,
			IBaseClient client,
			IEnumerable<Option> options = null)
			: base(requestUrl, client)
		{
			this.options = options;
		}
#pragma warning restore CA1054 // URI parameters should not be strings

		public ISearchRequest Request()
		{
			return this.Request(options);
		}

		public ISearchRequest Request(IEnumerable<Option> options)
		{
			return new SearchRequest(this.RequestUrl, this.Client, options);
		}

	}
}
