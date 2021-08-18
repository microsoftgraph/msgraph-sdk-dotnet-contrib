using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class RootSite
  {
    private readonly AzureAdSettings azureAdSettings;
    private readonly SharePointSettings sharePointSettings;

    public RootSite(
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
        UserAgent = "RootSiteSample"
      };
      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      //////////////////////////////////////

      try
      {
        var scopes = new string[] { "Sites.Read.All" };

        var results = await graphServiceClient
                        .Sites["root"]
                        .Request()
                        .Select(s => s.DisplayName)
                        .WithScopes(scopes)
                        .GetAsync();

        Console.WriteLine($"title: {results.DisplayName}");
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
