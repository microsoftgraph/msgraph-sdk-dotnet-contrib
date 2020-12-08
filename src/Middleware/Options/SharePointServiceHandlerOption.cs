using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class SharePointServiceHandlerOption : IMiddlewareOption
	{
		public bool DisableTelemetry { get; set; }
		public string ResourceUri { get; set; }
	}
}
