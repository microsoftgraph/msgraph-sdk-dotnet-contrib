using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using System;
using Graph.Community;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Net.Http;

namespace Graph.Community.Samples
{
	class Program
	{
		internal static AzureAdOptions azureAdOptions = new AzureAdOptions();

		static async Task Main(string[] args)
		{
			//await Diagnostics.Run();

			//await RootSite.Run();

			//await Search.Run();

			//await SiteGroups.Run();

			//await ChangeLog.Run();

			//await SiteDesign.Run();

			//await ImmutableIds.Run();

			await GraphGroupExtensions.Run();

			Console.WriteLine("Press enter to end");
			Console.ReadLine();
		}
	}
}
