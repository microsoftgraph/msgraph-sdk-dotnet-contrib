using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class ImmutableIds
  {
    private readonly AzureAdSettings azureAdSettings;
    private readonly SharePointSettings sharePointSettings;

    public ImmutableIds(
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

      var logger = new StringBuilderHttpMessageLogger();
      /*
			 *  Could also use the Console if preferred...
			 *  
			 *  var logger = new ConsoleHttpMessageLogger();
			 */

      // Configure our client
      CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
      {
        UserAgent = "ImmutableIdsSample"
      };

      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      ///////////////////////////////////////

      var scopes = new string[] { "https://graph.microsoft.com/Mail.Read" };

      var messages =
        await graphServiceClient
                .Me
                .Messages
                .Request()
                .Top(1)
                .WithScopes(scopes)
                .GetAsync();

      Console.WriteLine($"ID: {messages.CurrentPage[0].Id}");

      Console.WriteLine();

      var messagesI =
        await graphServiceClient
                .Me
                .Messages
                .Request()
                .WithImmutableId()
                .Top(1)
                .GetAsync();

      Console.WriteLine($"ImmutableId: {messagesI.CurrentPage[0].Id}");
      Console.WriteLine();

      Console.WriteLine("Press enter to show log");
      Console.ReadLine();
      Console.WriteLine();
      Console.WriteLine(logger.GetLog());
    }
  }
}
