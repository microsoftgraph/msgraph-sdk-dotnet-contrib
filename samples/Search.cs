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
	public static class Search
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

			var scopes = new string[] { $"https://{sharepointDomain}/AllSites.FullControl" };
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

				var queryText = $"adaptive";
				var propsToSelect = new List<string>() { "Title", "Path", "DocId]" };
				var sortList = new List<SearchQuery.Sort>() { new SearchQuery.Sort("DocId", SearchQuery.SortDirection.Ascending) };

				var query = new SearchQuery(
					queryText: queryText,
					selectProperties: propsToSelect,
					sortList: sortList);

				try
				{
					var results = await graphServiceClient
													.SharePointAPI(WebUrl)
													.Search
													.Request()
													.PostQueryAsync(query);

					var rowCount = results.PrimaryQueryResult.RelevantResults.RowCount;
					var totalRows = results.PrimaryQueryResult.RelevantResults.TotalRows;

					Console.WriteLine($"rowCount: {rowCount}");

					string lastDocId = null;
					foreach (var item in results.PrimaryQueryResult.RelevantResults.Table.Rows)
					{
						Console.WriteLine(item.Cells.FirstOrDefault(c => c.Key == "Path").Value);

						var docId = item.Cells.FirstOrDefault(c => c.Key == "DocId")?.Value;
						if (docId != null)
						{
							lastDocId = docId;
						}
					}

					if (totalRows > rowCount && !string.IsNullOrEmpty(lastDocId))
					{
						var nextPageQuery = new SearchQuery(
							queryText: $"{queryText} indexdocid>{lastDocId}",
							selectProperties: propsToSelect,
							sortList: sortList);

						var page2results = await graphServiceClient
													.SharePointAPI(WebUrl)
													.Search
													.Request()
													.PostQueryAsync(nextPageQuery);

						foreach (var item in page2results.PrimaryQueryResult.RelevantResults.Table.Rows)
						{
							Console.WriteLine(item.Cells.FirstOrDefault(c => c.Key == "Path").Value);

						}
					}
						Console.WriteLine($"totalRows: {totalRows}");
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
