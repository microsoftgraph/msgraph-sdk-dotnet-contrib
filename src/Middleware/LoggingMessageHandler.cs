﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public class LoggingMessageHandler : DelegatingHandler
	{
		internal StringBuilder output = new StringBuilder();

		public LoggingMessageHandler()
		{
		}

		public LoggingMessageHandler(HttpMessageHandler innerHandler)
		{
			InnerHandler = innerHandler;
		}

		public string Log
		{
			get
			{
				var log = output.ToString();
				output.Clear();
				return log;
			}
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request.Content != null)
			{
				await request.Content.LoadIntoBufferAsync();
			}

			var messageFormatter = new HttpMessageFormatter(request);
			if (output != null)
			{
				output.AppendLine(await messageFormatter.ReadAsStringAsync());
				output.AppendLine("");
			}

			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var response = await base.SendAsync(request, cancellationToken);

			stopWatch.Stop();

			if (response.Content != null)
			{
				await response.Content.LoadIntoBufferAsync();
			}
			messageFormatter = new HttpMessageFormatter(response);

			if (output != null)
			{
				output.AppendLine(await messageFormatter.ReadAsStringAsync());
				output.AppendLine("");
				output.AppendLine("Roundtrip (ms): " + stopWatch.ElapsedMilliseconds);
				output.AppendLine("================================================");
			}

			return response;
		}

	}
}