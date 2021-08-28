using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class SiteDesign
	{
		private readonly AzureAdSettings azureAdSettings;
		private readonly SharePointSettings sharePointSettings;

		public SiteDesign(
			IOptions<AzureAdSettings> azureAdOptions,
			IOptions<SharePointSettings> sharePointOptions)
		{
			this.azureAdSettings = azureAdOptions.Value;
			this.sharePointSettings = sharePointOptions.Value;
		}

		public async Task Run()
		{
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

			var logger = new StringBuilderHttpMessageLogger();
			/*
			 *  Could also use the Console if preferred...
			 *  
			 *  var logger = new ConsoleHttpMessageLogger();
			 */

			// Configure our client
			CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
			{
				UserAgent = "SiteDesignSample"
			};
			var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

			///////////////////////////////////////
			//
			// Setup is complete, run the sample
			//
			///////////////////////////////////////

			var scopes = new string[] { $"https://{sharePointSettings.Hostname}/AllSites.FullControl" };
			var WebUrl = $"https://{sharePointSettings.Hostname}{sharePointSettings.SiteCollectionUrl}";

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
																	.WithScopes(scopes)
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
																	.WithScopes(scopes)
																	.CreateAsync(siteDesign);

			var applySiteDesignRequest = new ApplySiteDesignRequest
			{
				SiteDesignId = createdDesign.Id,
				WebUrl = WebUrl
			};

			var applySiteDesignResponse = await graphServiceClient
																						.SharePointAPI(WebUrl)
																						.SiteDesigns
																						.Request()
																						.WithScopes(scopes)
																						.ApplyAsync(applySiteDesignRequest);

			foreach (var outcome in applySiteDesignResponse.CurrentPage)
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
