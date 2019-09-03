using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class SiteUserRequestBuilder : BaseRequestBuilder, ISiteUserRequestBuilder
	{
		private IEnumerable<Option> options;

		public SiteUserRequestBuilder(
			string requestUrl,
			IBaseClient client,
			IEnumerable<Option> options = null)
			: base(requestUrl, client)
		{
		}


		public ISiteUserRequest Request()
		{
			return this.Request(options);
		}

		public ISiteUserRequest Request(IEnumerable<Option> options)
		{
			return new SiteUserRequest(this.RequestUrl, this.Client, options);
		}

	}
}
