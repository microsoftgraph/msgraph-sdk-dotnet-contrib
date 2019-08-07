using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community.Samples
{
	public static class ChangeLog
	{
		public static void Run(GraphServiceClient graphServiceClient)
		{
			var WebUrl = $"https://[SharePointDomain].sharepoint.com/sites/ChangeLogTest";

			var web = graphServiceClient
									.SharePointAPI(WebUrl)
									.Web
									.Request()
									.GetAsync()
									.GetAwaiter().GetResult();

			var changeToken = web.CurrentChangeToken;
			Console.WriteLine($"current change token: {changeToken.StringValue}");

			Console.WriteLine($"Make an update to the site {WebUrl}");
			Console.WriteLine("Press enter to continue");
			Console.ReadLine();

			var qry = new ChangeQuery(true, true);
			qry.ChangeTokenStart = changeToken;

			var changes = graphServiceClient
											.SharePointAPI(WebUrl)
											.Web
											.Request()
											.GetChangesAsync(qry)
											.GetAwaiter().GetResult();

			Console.WriteLine(changes.Count);

			foreach (var item in changes)
			{
				Console.WriteLine($"{item.ChangeType}");
			}

			Console.WriteLine();
			Console.WriteLine("Press enter to continue");
			Console.ReadLine();
		}
	}
}
