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
	public static class ExpiringClientSecrets
	{
		public static async Task Run()
		{
			////////////////////////////////
			//
			// Azure AD Configuration
			//
			////////////////////////////////

			AzureAdOptions azureAdOptions = new AzureAdOptions();

			var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
			var builder = new ConfigurationBuilder()
													.AddJsonFile(settingsFilename, optional: false)
													.AddUserSecrets<Program>();
			var config = builder.Build();
			config.Bind("AzureAd", azureAdOptions);

			/////////////////////////////////////
			//
			// Client Application Configuration
			//
			/////////////////////////////////////

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

			///////////////////////////////////////////////
			//
			//  Auth Provider - Interactive in this sample
			//
			///////////////////////////////////////////////

			// Use the system browser to login
			//  https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core#how-to-use-the-system-browser-ie-the-default-browser-of-the-os

			// Create an authentication provider to attach the token to requests
			var scopes = new string[] { "https://graph.microsoft.com/Directory.AccessAsUser.All" };
			IAuthenticationProvider ap = new InteractiveAuthenticationProvider(pca, scopes);

			////////////////////////////////////////////////////////////////
			//
			//  Create a GraphClient 
			//
			////////////////////////////////////////////////////////////////

			// Configure our client
			CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
			{
				UserAgent = "ExpiringClientSecretsSample"
			};

			var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, ap);

			///////////////////////////////////////
			//
			// Setup is complete, run the sample
			//
			///////////////////////////////////////


			bool iteratorItemCallback(Application a)    // equivalent to Func<Application, bool> iteratorItemCallback = (a) => {}
			{
        // process the current item
        if (a.PasswordCredentials.Any(c => c.EndDateTime < DateTime.UtcNow.AddDays(30)))
        {
					Console.WriteLine($"{a.DisplayName} ({a.AppId})");
        }


				// return true to indicate iteration should continue
				return true;
			}

			var results = await graphServiceClient
											.Applications
											.Request()
											.Top(999)
											.GetAsync();

			var appIterator = PageIterator<Application>.CreatePageIterator(graphServiceClient, results, iteratorItemCallback);

			await appIterator.IterateAsync();

		}
	}
}
