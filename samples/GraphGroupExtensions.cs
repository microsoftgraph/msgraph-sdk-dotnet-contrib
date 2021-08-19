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

      Console.WriteLine("Enter the UPN of new group owner");
      var ownerUpn = Console.ReadLine();

      try
      {
        var scopes = new string[] { "https://graph.microsoft.com/Directory.AccessAsUser.All" };

        var u = await graphServiceClient.Users[$"{ownerUpn}"].Request().GetAsync();

        var g = new Microsoft.Graph.Group
        {
          DisplayName = "Graph.Community Extension Sample",
          MailEnabled = false,
          MailNickname = "gce-sample",
          SecurityEnabled = true
        };

        g.AddMember(u.Id);
        g = await graphServiceClient.Groups.Request().AddAsync(g);

        Console.WriteLine($"Group: {g.DisplayName} ({g.Id})");

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
