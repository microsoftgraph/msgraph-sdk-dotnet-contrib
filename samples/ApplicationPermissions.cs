using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class ApplicationPermissions
  {
    private readonly AzureAdSettings azureAdSettings;

    public ApplicationPermissions(
      IOptions<AzureAdSettings> azureAdOptions)
    {
      this.azureAdSettings = azureAdOptions.Value;
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
      //  Create a GraphClient 
      //
      ////////////////////////////////////////////////////////////////

      // Configure our client
      CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
      {
        UserAgent = "ApplicationPermissionsSample"
      };

      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      ///////////////////////////////////////


      Console.WriteLine("Enter application id:");
      var appId = Console.ReadLine().Trim();

      var scopes = new string[] { "Application.Read.All" };

      var results = await graphServiceClient
                      .Applications
                      .Request()
                      .Filter($"appId eq '{appId}'")
                      .WithScopes(scopes)
                      .GetAsync();

      var app = results.CurrentPage.FirstOrDefault();
      if (app == null)
      {
        Console.WriteLine("App not found");
        return;
      }

      foreach (var requiredResourceAccess in app.RequiredResourceAccess)
      {
        Console.WriteLine(requiredResourceAccess.ResourceAppId);
      }

    }
  }
}
