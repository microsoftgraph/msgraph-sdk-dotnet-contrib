using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class WebRequestBuilder : BaseRequestBuilder, IWebRequestBuilder
	{
		private IEnumerable<Option> options;

		public WebRequestBuilder(
			string requestUrl,
			IBaseClient client,
			IEnumerable<Option> options = null)
			: base(requestUrl, client)
		{
			this.options = options;
		}

		public IListRequestBuilder Lists
		{
			get
			{
				return new ListRequestBuilder(this.RequestUrl, this.Client, this.options);
			}
		}

		public IWebRequest Request()
		{
			return this.Request(options);
		}

		public IWebRequest Request(IEnumerable<Option> options)
		{
			return new WebRequest(this.RequestUrl, this.Client, options);
		}
	}
}
