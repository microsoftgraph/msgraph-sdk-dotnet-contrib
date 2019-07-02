using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public interface ISharePointAPIRequestBuilder : IBaseRequestBuilder
	{
		ISiteDesignRequestBuilder SiteDesigns { get; }

		ISiteRequestBuilder Site { get; }

		IWebRequestBuilder Web { get; }
	}
}
