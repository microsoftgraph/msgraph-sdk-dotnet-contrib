using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class YammerReports
  {
    private readonly AzureAdSettings azureAdSettings;

    public YammerReports(
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

      ////////////////////////////////////////////////////////////
      //
      // Graph Client with Logger and SharePoint service handler
      //
      ////////////////////////////////////////////////////////////

      var logger = new StringBuilderHttpMessageLogger();

      // Configure our client
      CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
      {
        UserAgent = "YammerReports"
      };

      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      ///////////////////////////////////////

      var scopes = new string[] { "Reports.Read.All" };

      try
      {
        var report = await graphServiceClient.Reports
                            .GetYammerGroupsActivityCounts("D30")
                            .Request()
                            .WithScopes(scopes)
                            .GetAsync();

        //var requestMessage = graphServiceClient.Reports
        //                .GetYammerGroupsActivityCounts("D30")
        //                .Request()
        //                .WithScopes(scopes)
        //                .GetHttpRequestMessage();


        //Console.WriteLine(report.ODataType);
      }
      catch (Exception ex)
      {

      }


      Console.WriteLine();
      Console.WriteLine("Press enter to show log");
      Console.ReadLine();
      Console.WriteLine();
      var log = logger.GetLog();
      Console.WriteLine(log);

    }

  }
}
