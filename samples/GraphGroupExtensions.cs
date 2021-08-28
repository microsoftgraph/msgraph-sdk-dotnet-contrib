using Azure.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
	public class GraphGroupExtensions
	{
		private readonly AzureAdSettings azureAdSettings;
		private readonly SharePointSettings sharePointSettings;

		public GraphGroupExtensions(
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

			////////////////////////////////////////////////////////////////
			//
			//  Create a GraphClient with the Logging handler
			//
			////////////////////////////////////////////////////////////////

			// Log Http Request/Response
			var logger = new StringBuilderHttpMessageLogger();

			// Configure our client
			CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
			{
				UserAgent = "GraphGroupExtensionSample"
			};

			var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

			///////////////////////////////////////
			//
			// Setup is complete, run the sample
			//
			///////////////////////////////////////

			Console.WriteLine("Enter a name for the new group.");
			var groupName = Console.ReadLine().Trim();

			if (string.IsNullOrEmpty(groupName))
			{
				Console.WriteLine("Group name is required.");
				return;
			}

			Console.WriteLine("Enter the UPN of a user. The User will be added as a group owner and member");
			var userUpn = Console.ReadLine().Trim();
			if (string.IsNullOrEmpty(userUpn))
			{
				Console.WriteLine("User UPN is required.");
				return;
			}

			try
			{
				var scopes = new string[] { "https://graph.microsoft.com/Directory.AccessAsUser.All" };

				var u = await graphServiceClient.Users[$"{userUpn}"].Request().GetAsync();

				var g = new Microsoft.Graph.Group
				{
					DisplayName = groupName,
					MailEnabled = false,
					MailNickname = groupName.Replace(" ","").ToLower(),
					SecurityEnabled = true
				};

				// This extension method adds the user to the collection in the in-memory Group object
				g.AddOwner(u.Id);
				g.AddMember(u.Id);
				g = await graphServiceClient.Groups.Request().AddAsync(g);

				Console.WriteLine($"Group: {g.DisplayName} ({g.Id})");


				Console.WriteLine("Press enter to remove the user");
				Console.ReadLine();

				// Cannot remove last owner of a group, so commented
				//await graphServiceClient.Groups[g.Id].Owners.Request().RemoveAsync(u.Id);
				await graphServiceClient.Groups[g.Id].Members.Request().RemoveAsync(u.Id);

				Console.WriteLine("Press enter to delete the group");
				Console.ReadLine();

				await graphServiceClient.Groups[g.Id].Request().DeleteAsync();
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
