using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class SiteGroups
  {
    private readonly AzureAdSettings azureAdSettings;
    private readonly SharePointSettings sharePointSettings;

    public SiteGroups(
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
        UserAgent = "SiteGroupsSample"
      };
      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      ///////////////////////////////////////

      var scopes = new string[] { $"https://{sharePointSettings.Hostname}/AllSites.FullControl" };
      var WebUrl = $"https://{sharePointSettings.Hostname}{sharePointSettings.SiteCollectionUrl}";

      var groups = await graphServiceClient
                      .SharePointAPI(WebUrl)
                      .Web
                      .SiteGroups
                      .Request()
                      .Expand(g => g.Users)
                      .Expand("Owner")
                      .WithScopes(scopes)
                      .GetAsync();


      foreach (var group in groups)
      {
        Console.WriteLine(group.Title);
        foreach (var user in group.Users)
        {
          Console.WriteLine($"  {user.LoginName}");
        }
      }

      Console.WriteLine("Press enter to show log");
      Console.ReadLine();
      Console.WriteLine();
      var log = logger.GetLog();
      Console.WriteLine(log);
    }
  }

}
