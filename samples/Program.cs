using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using System;
using Graph.Community;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Graph.Community.Samples.CommandLine
{
	class Program
	{
		internal static AzureAdOptions azureAdOptions = new AzureAdOptions();

		static void Main(string[] args)
		{
			GetConfiguration();

			using (LoggingMessageHandler logger = new LoggingMessageHandler())
			using (HttpProvider hp = new HttpProvider(logger, false, new Serializer()))
			{
				GraphServiceClient graphServiceClient = GetGraphServiceClient(hp, new string[] { "https://[SharePointDomain].sharepoint.com/AllSites.FullControl" });  

				ChangeLog.Run(graphServiceClient);
				Console.WriteLine(logger.Log);

				SiteDesign.Run(graphServiceClient);
				Console.WriteLine(logger.Log);

			}

			using (LoggingMessageHandler logger = new LoggingMessageHandler())
			using (HttpProvider hp = new HttpProvider(logger, false, new Serializer()))
			{
				GraphServiceClient graphServiceClient = GetGraphServiceClient(hp, new string[] { "https://graph.microsoft.com/Mail.Read" });

				ImmutableIds.Run(graphServiceClient);
			}
		}


		public static GraphServiceClient GetGraphServiceClient(HttpProvider hp, string[] scopes)
		{
			string authority = $"https://login.microsoftonline.com/{azureAdOptions.TenantId}";

			var pca = PublicClientApplicationBuilder
									.Create(azureAdOptions.ClientId)
									.WithTenantId(azureAdOptions.TenantId)
									.Build();

			IAuthenticationProvider ap = new DeviceCodeProvider(pca, scopes);
			
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
