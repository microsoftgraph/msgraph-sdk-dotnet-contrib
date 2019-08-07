using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community.Samples
{
	public static class SiteDesign
	{
		public static void Run(GraphServiceClient graphServiceClient)
		{
			var WebUrl = "https://[SharePointDomain].sharepoint.com/sites/SiteDesignTest";

			var siteScript = new SiteScriptMetadata()
			{
				Title = "Green Theme",
				Description = "Apply the Green Theme",
				Content = "{\"$schema\": \"schema.json\",\"actions\": [{\"verb\": \"applyTheme\",\"themeName\": \"Green\"}],\"bindata\": { },\"version\": 1}",
			};

			var createdScript = graphServiceClient
														.SharePointAPI(WebUrl)
														.SiteScripts
														.Request()
														.CreateAsync(siteScript)
														.GetAwaiter().GetResult();

			var siteDesign = new SiteDesignMetadata()
			{
				Title = "Green Theme",
				Description = "Apply the Green theme",
				SiteScriptIds = new System.Collections.Generic.List<Guid>() { new Guid(createdScript.Id) },
				WebTemplate = "64" // 64 = Team Site, 68 = Communication Site, 1 = Groupless Team Site
			};

			var createdDesign = graphServiceClient
														.SharePointAPI(WebUrl)
														.SiteDesigns
														.Request()
														.CreateAsync(siteDesign)
														.GetAwaiter().GetResult();

			var applySiteDesignRequest = new ApplySiteDesignRequest
			{
				SiteDesignId = createdDesign.Id,
				WebUrl = WebUrl
			};

			var applySiteDesignResponse = graphServiceClient
																			.SharePointAPI(WebUrl)
																			.SiteDesigns.Request()
																			.ApplyAsync(applySiteDesignRequest)
																			.GetAwaiter().GetResult();

			foreach (var outcome in applySiteDesignResponse.ActionOutcomes)
			{
				Console.WriteLine(outcome.OutcomeText);
			}
			Console.WriteLine();
			Console.WriteLine("Press enter to continue");
			Console.ReadLine();

		}
	}
}
