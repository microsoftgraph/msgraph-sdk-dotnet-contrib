using Azure.Core.Diagnostics;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class Diagnostics
	{
		private readonly AzureAdSettings azureAdSettings;
		private readonly SharePointSettings sharePointSettings;

		public Diagnostics(
			IOptions<AzureAdSettings> azureAdOptions,
			IOptions<SharePointSettings> sharePointOptions)
		{
			this.azureAdSettings = azureAdOptions.Value;
			this.sharePointSettings = sharePointOptions.Value;
		}


		public async Task Run()
		{
			////////////////////////////////////////
			//
			// Capture all diagnostic information
			//
			///////////////////////////////////////

			// Start with an IHttpMessageLogger that will write to a StringBuilder 
			var logger = new StringBuilderHttpMessageLogger();
			/*
			 *  Could also use the Console if preferred...
			 *  
			 *  var logger = new ConsoleHttpMessageLogger();
			 */


			// MSAL provides logging via a callback on the client application.
			//  Write those entries to the same logger, prefixed with MSAL
			//async void MSALLogging(LogLevel level, string message, bool containsPii)
			//{
			//	await logger.WriteLine($"MSAL {level} {containsPii} {message}");
			//}


			// AzureSDK uses an EventSource to publish diagnostics in the token acquisition.
			// Setup a listener to monitor logged events.
			//using AzureEventSourceListener azListener = AzureEventSourceListener.CreateConsoleLogger();
			var azListener = new AzureEventSourceListener(async (args, message) =>
			{
				// create a dictionary of the properties of the args object
				var properties = args.PayloadNames
													.Zip(args.Payload, (string k, object v) => new { Key = k, Value = v })
													.ToDictionary(x => x.Key, x => x.Value.ToString());

				// log the message and payload, prefixed with COMM
				var traceMessage = string.Format(args.Message, args.Payload.ToArray());
				await logger.WriteLine($"AZ {traceMessage}");
			}, System.Diagnostics.Tracing.EventLevel.LogAlways);

			// GraphCommunity uses an EventSource to publish diagnostics in the handler.
			//    This follows the pattern used by the Azure SDK.
			var listener = new Community.Diagnostics.GraphCommunityEventSourceListener(async (args, message) =>
			{
				if (args.EventSource.Name.StartsWith("Graph-Community"))
				{
					// create a dictionary of the properties of the args object
					var properties = args.PayloadNames
														.Zip(args.Payload, (string k, object v) => new { Key = k, Value = v })
														.ToDictionary(x => x.Key, x => x.Value.ToString());

					// log the message and payload, prefixed with COMM
					var traceMessage = string.Format(args.Message, args.Payload.ToArray());
					await logger.WriteLine($"COMM {traceMessage}");
				}
			}, System.Diagnostics.Tracing.EventLevel.LogAlways);


			//////////////////////
			//
			//  TokenCredential 
			//
			//////////////////////

			var credential = new ChainedTokenCredential(
				new SharedTokenCacheCredential(new SharedTokenCacheCredentialOptions() { TenantId = azureAdSettings.TenantId, ClientId = azureAdSettings.ClientId }),
				new VisualStudioCredential(new VisualStudioCredentialOptions { TenantId = azureAdSettings.TenantId }),
				new InteractiveBrowserCredential(new InteractiveBrowserCredentialOptions { TenantId = azureAdSettings.TenantId, ClientId = azureAdSettings.ClientId })
			);


			////////////////////////////////////////////////////////////
			//
			// Graph Client with Logger and SharePoint service handler
			//
			////////////////////////////////////////////////////////////

			// Configure our client
			CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
			{
				UserAgent = "DiagnosticsSample"
			};

			var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);


			///////////////////////////////////////
			//
			// Setup is complete, run the sample
			//
			///////////////////////////////////////

			try
			{
				var scopes = new string[] { $"https://{sharePointSettings.Hostname}/AllSites.FullControl" };
				var WebUrl = $"https://{sharePointSettings.Hostname}{sharePointSettings.SiteCollectionUrl}";

				var appTiles = await graphServiceClient
												.SharePointAPI(WebUrl)
												.Web
												.AppTiles
												.Request()
												.WithScopes(scopes)
												.GetAsync();

				Console.WriteLine($"Tile count: {appTiles.Count}");

				var me = await graphServiceClient
										.Me
										.Request()
										.WithScopes(new string[] { "https://graph.microsoft.com/User.Read" })
										.GetAsync();

				Console.WriteLine($"Me.DisplayName: {me.DisplayName}");
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

