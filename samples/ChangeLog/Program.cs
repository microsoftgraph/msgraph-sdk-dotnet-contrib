using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Graph.Community.Samples.CommandLine.Utilities;
using System;
using Graph.Community;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Graph.Community.Samples.CommandLine
{
	class Program
	{
		internal static AzureAdOptions azureAdOptions = new AzureAdOptions();
		internal static LoggingMessageHandler logger;
		internal static HttpProvider hp;

		static void Main(string[] args)
		{
			GetConfiguration();

			using (logger = new LoggingMessageHandler())
			using (hp = new HttpProvider(logger, false, new Serializer()))
			{
				var graphServiceClient = GetGraphServiceClient("DeviceCode");

				var WebUrl = $"https://{azureAdOptions.SharePointDomain}.sharepoint.com/sites/NavigationCommTest";

				var web = graphServiceClient
										.SharePointAPI(WebUrl)
										.Web
										.Request()
										//.WithUsernamePassword(azureAdOptions.Username, azureAdOptions.SecurePassword)
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
												//.WithUsernamePassword(azureAdOptions.Username, azureAdOptions.SecurePassword)
												.GetChangesAsync(qry)
												.GetAwaiter().GetResult();

				Console.WriteLine(changes.Count);

				foreach (var item in changes)
				{
					Console.WriteLine($"{item.ChangeType}");
				}

				Console.WriteLine();
				Console.Write(logger.Log);
				Console.WriteLine();

			}
		}

		private static GraphServiceClient GetGraphServiceClient(string authProviderTypename)
		{
			string authority = $"https://login.microsoftonline.com/{azureAdOptions.TenantId}";
			string[] scopes = azureAdOptions.Scopes;

			var pca = PublicClientApplicationBuilder
									.Create(azureAdOptions.ClientId)
									.WithTenantId(azureAdOptions.TenantId)
									.Build();

			IAuthenticationProvider ap = default;
			if (authProviderTypename == "UsernamePassword")
			{
				ap = new UsernamePasswordProvider(pca, scopes);
			}
			if (authProviderTypename == "DeviceCode")
			{
				ap = new DeviceCodeProvider(pca, scopes);
			}

			var graphServiceClient = new GraphServiceClient(ap, hp);
			return graphServiceClient;
		}

		private static void GetConfiguration()
		{
			var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
			var builder = new ConfigurationBuilder()
													.AddJsonFile(settingsFilename, optional: false)
													.AddUserSecrets<Program>();
			var config = builder.Build();
			config.Bind("AzureAd", azureAdOptions);
		}
	}
}
