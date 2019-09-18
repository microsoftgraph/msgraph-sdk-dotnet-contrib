using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
	class StringBuilderHttpMessageLogger : IHttpMessageLogger
	{
		private readonly StringBuilder sb = new StringBuilder();

		public string GetLog()
		{
			var log = sb.ToString();
			sb.Clear();
			return log;
		}

		public async Task WriteLine(string value)
		{
			sb.AppendLine(value);
		}
	}
}
