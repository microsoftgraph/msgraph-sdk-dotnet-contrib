using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public interface IWebRequestBuilder
	{
		IWebRequest Request();

		IListRequestBuilder Lists { get; } 
	}
}
