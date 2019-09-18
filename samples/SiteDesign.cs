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
	public static class SiteDesign
	{
		public static async Task Run()
		{
			/////////////////////////////
			//
			// Programmer configuration
			//
			/////////////////////////////

			var sharepointDomain = "demo.sharepoint.com";
			var siteCollectionPath = "/sites/SiteDesignTest";

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

			var scopes = new string[] { $"https://{sharepointDomain}/AllSites.FullControl" };
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

				var WebUrl = $"https://{sharepointDomain}{siteCollectionPath}";

				var siteScript = new SiteScriptMetadata()
				{
					Title = "Green Theme",
					Description = "Apply the Green Theme",
					Content = "{\"$schema\": \"schema.json\",\"actions\": [{\"verb\": \"applyTheme\",\"themeName\": \"Green\"}],\"bindata\": { },\"version\": 1}",
				};

				var createdScript = await graphServiceClient
																		.SharePointAPI(WebUrl)
																		.SiteScripts
																		.Request()
																		.CreateAsync(siteScript);

				var siteDesign = new SiteDesignMetadata()
				{
					Title = "Green Theme",
					Description = "Apply the Green theme",
					SiteScriptIds = new System.Collections.Generic.List<Guid>() { new Guid(createdScript.Id) },
					WebTemplate = "64" // 64 = Team Site, 68 = Communication Site, 1 = Groupless Team Site
				};

				var createdDesign = await graphServiceClient
																		.SharePointAPI(WebUrl)
																		.SiteDesigns
																		.Request()
																		.CreateAsync(siteDesign);

				var applySiteDesignRequest = new ApplySiteDesignRequest
				{
					SiteDesignId = createdDesign.Id,
					WebUrl = WebUrl
				};

				var applySiteDesignResponse = await graphServiceClient
																							.SharePointAPI(WebUrl)
																							.SiteDesigns.Request()
																							.ApplyAsync(applySiteDesignRequest);

				foreach (var outcome in applySiteDesignResponse.ActionOutcomes)
				{
					Console.WriteLine(outcome.OutcomeText);
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
