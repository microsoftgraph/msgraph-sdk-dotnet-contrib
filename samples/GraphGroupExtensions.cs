using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
	public static class GraphGroupExtensions
	{
		public static async Task Run()
		{
			/////////////////
			//
			// Configuration
			//
			/////////////////

			AzureAdOptions azureAdOptions = new AzureAdOptions();

			var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
			var builder = new ConfigurationBuilder()
													.AddJsonFile(settingsFilename, optional: false);
			var config = builder.Build();
			config.Bind("AzureAd", azureAdOptions);

			// Log Http Request/Response
			var logger = new StringBuilderHttpMessageLogger();

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
			var pca = PublicClientApplicationBuilder.CreateWithApplicationOptions(options)
					.Build();

			// Enable a simple token cache serialiation so that the user does not need to
			// re-sign-in each time the application is run
			TokenCacheHelper.EnableSerialization(pca.UserTokenCache);

			// Create an authentication provider to attach the token to requests
			var scopes = new string[] { "https://graph.microsoft.com/Directory.AccessAsUser.All" };
			IAuthenticationProvider ap = new InteractiveAuthenticationProvider(pca, scopes);


			////////////////////////////////////////////////////////////////
			//
			//  Create a GraphClient with the Logging handler
			//
			////////////////////////////////////////////////////////////////

			// Configure our client
			CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
			{
				UserAgent = "GraphGroupExtensionSample"
			};

			var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, ap);


			////////////////////////////
			//
			// Setup is complete, run the sample
			//
			////////////////////////////

			try
			{
				var u = await graphServiceClient.Users["EXISTING-USER-GUID-OR-UPN"].Request().GetAsync();

				var g = new Microsoft.Graph.Group
				{
					DisplayName = "Graph.Community Extension Sample",
					MailEnabled = false,
					MailNickname = "gce-sample",
					SecurityEnabled = true
				};

				g.AddMember(u.Id);
				g = await graphServiceClient.Groups.Request().AddAsync(g);

				Console.WriteLine($"Group: {g.DisplayName} ({g.Id})");

			}
			catch (Exception ex)
			{
				await logger.WriteLine("");
				await logger.WriteLine("================== Exception caught ==================");
				await logger.WriteLine(ex.ToString());
			}


			Console.WriteLine("Press enter to show log");
			Console.ReadLine();
			Console.WriteLine();
			var log = logger.GetLog();
			Console.WriteLine(log);
		}
	}

}
