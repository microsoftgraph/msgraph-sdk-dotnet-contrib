using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
	public static class RootSite
	{
		public static async Task Run()
		{
      /////////////////////////////
      //
      // Programmer configuration
      //
      /////////////////////////////

      var sharepointDomain = "demo.sharepoint.com";
      var siteCollectionPath = "/sites/GraphCommunityDemo";

			/////////////////
			//
			// Configuration
			//
			/////////////////

			AzureAdOptions azureAdOptions = new AzureAdOptions();

			var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
			var builder = new ConfigurationBuilder()
													.AddJsonFile(settingsFilename, optional: false)
													.AddUserSecrets<Program>();
			var config = builder.Build();
			config.Bind("AzureAd", azureAdOptions);

			////////////////////////////
			//
			// Graph Client with Logger
			//
			////////////////////////////

			var logger = new StringBuilderHttpMessageLogger();
			/*
			 *  Could also use the Console if preferred...
			 *  
			 *  var logger = new ConsoleHttpMessageLogger();
			 */


			// Use the system browser to login
			//  https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core#how-to-use-the-system-browser-ie-the-default-browser-of-the-os

			var options = new PublicClientApplicationOptions()
			{
				AadAuthorityAudience = AadAuthorityAudience.AzureAdMyOrg,
				AzureCloudInstance = AzureCloudInstance.AzurePublic,
				ClientId = azureAdOptions.ClientId,
				TenantId = azureAdOptions.TenantId,
				RedirectUri = "http://localhost"
			};

			// Create the public client application (desktop app), with a default redirect URI
			var pca = PublicClientApplicationBuilder
									.CreateWithApplicationOptions(options)
									.Build();

			// Enable a simple token cache serialiation so that the user does not need to
			// re-sign-in each time the application is run
			TokenCacheHelper.EnableSerialization(pca.UserTokenCache);

			var scopes = new string[] { $"https://graph.microsoft.com/Sites.Read.All" };
			IAuthenticationProvider ap = new InteractiveAuthenticationProvider(pca, scopes);

			using (LoggingMessageHandler loggingHandler = new LoggingMessageHandler(logger))
			using (HttpProvider hp = new HttpProvider(loggingHandler, false, new Serializer()))
			{
				GraphServiceClient graphServiceClient = new GraphServiceClient(ap, hp);

				////////////////////////////
				//
				// Setup is complete, run the sample
				//
				////////////////////////////

				var WebUrl = $"https://{sharepointDomain}{siteCollectionPath}";

				try
				{
					var results = await graphServiceClient
													.Sites["root"]
													.Request()
													.Select(s => s.DisplayName)
													.GetAsync();

					Console.WriteLine($"title: {results.DisplayName}");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}



				Console.WriteLine("Press enter to show log");
				Console.ReadLine();
				Console.WriteLine();
				var log = logger.GetLog();
				Console.WriteLine(log);
			}

		}
	}
}
