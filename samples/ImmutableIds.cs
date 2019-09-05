using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
	public static class ImmutableIds
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

			var pca = PublicClientApplicationBuilder
									.Create(azureAdOptions.ClientId)
									.WithTenantId(azureAdOptions.TenantId)
									.Build();

			var scopes = new string[] { "https://graph.microsoft.com/Mail.Read" };
			IAuthenticationProvider ap = new DeviceCodeProvider(pca, scopes);

			using (LoggingMessageHandler loggingHandler = new LoggingMessageHandler(logger))
			using (HttpProvider hp = new HttpProvider(loggingHandler, false, new Serializer()))
			{
				GraphServiceClient graphServiceClient = new GraphServiceClient(ap, hp);


				////////////////////////////
				//
				// Setup is complete, run the sample
				//
				////////////////////////////

				var messages =
					await graphServiceClient
									.Me
									.Messages
									.Request()
									.Top(1)
									.GetAsync();

				Console.WriteLine($"ID: {messages.CurrentPage[0].Id}");

				Console.WriteLine();

				var messagesI =
					await graphServiceClient
									.Me
									.Messages
									.Request()
									.WithImmutableId()
									.Top(1)
									.GetAsync();

				Console.WriteLine($"ImmutableId: {messagesI.CurrentPage[0].Id}");
				Console.WriteLine();

				Console.WriteLine("Press enter to show log");
				Console.ReadLine();
				Console.WriteLine();
				Console.WriteLine(logger.GetLog());
			}
		}
	}
}
