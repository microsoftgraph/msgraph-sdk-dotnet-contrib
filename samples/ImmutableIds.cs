using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community.Samples
{
	public static class ImmutableIds
	{
		public static void Run(GraphServiceClient graphServiceClient)
		{
			var messages = graphServiceClient
											.Me
											.Messages
											.Request()
											.GetAsync()
											.GetAwaiter().GetResult();
			Console.WriteLine($"ID: {messages.CurrentPage[0].Id}");

			Console.WriteLine();

			var messagesI = graphServiceClient
												.Me
												.Messages
												.Request()
												.WithImmutableId()
												.GetAsync()
												.GetAwaiter().GetResult();
			Console.WriteLine($"ImmutableId: {messagesI.CurrentPage[0].Id}");

			Console.WriteLine();
			Console.WriteLine("Press enter to continue");
			Console.ReadLine();

		}
	}
}
