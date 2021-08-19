using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class ExpiringClientSecrets
  {
    private readonly AzureAdSettings azureAdSettings;
    private readonly SharePointSettings sharePointSettings;

    public ExpiringClientSecrets(
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
      //  Create a GraphClient 
      //
      ////////////////////////////////////////////////////////////////

      // Configure our client
      CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
      {
        UserAgent = "ExpiringClientSecretsSample"
      };

      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      ///////////////////////////////////////


      bool iteratorItemCallback(Application a)    // equivalent to Func<Application, bool> iteratorItemCallback = (a) => {}
      {
        // process the current item
        if (a.PasswordCredentials.Any(c => c.EndDateTime < DateTime.UtcNow.AddDays(30)))
        {
          Console.WriteLine($"{a.DisplayName} ({a.AppId})");
        }


        // return true to indicate iteration should continue
        return true;
      }

      var scopes = new string[] { "Application.Read.All" };

      var results = await graphServiceClient
                      .Applications
                      .Request()
                      .Top(999)
                      .WithScopes(scopes)
                      .GetAsync();

      var appIterator = PageIterator<Application>.CreatePageIterator(graphServiceClient, results, iteratorItemCallback);

      await appIterator.IterateAsync();

    }
  }
}
