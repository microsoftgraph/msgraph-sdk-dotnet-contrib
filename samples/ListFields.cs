using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class ListFields
  {
    private readonly AzureAdSettings azureAdSettings;
    private readonly SharePointSettings sharePointSettings;

    public ListFields(
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
        //new SharedTokenCacheCredential(new SharedTokenCacheCredentialOptions() { TenantId = azureAdSettings.TenantId, ClientId = azureAdSettings.ClientId }),
        //new VisualStudioCredential(new VisualStudioCredentialOptions { TenantId = azureAdSettings.TenantId }),
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

      try
      {
        var fieldsResult = await graphServiceClient
                          .SharePointAPI(WebUrl)
                          .Web
                          .Lists[new Guid("5387d82e-062e-4267-a60e-840f89f947d8")]
                          .Fields
                          .Request()
                          .WithScopes(scopes)
                          .GetAsync();

        // SharePoint doesn't return nextPage link, so can't use iterator
        //var pageIterator = PageIterator<Field>.CreatePageIterator(graphServiceClient, fieldsResult, iteratorItemCallback, iteratorRequestConfigurator);
        //await pageIterator.IterateAsync();

        Console.WriteLine();
        Console.WriteLine();

        foreach (var field in fieldsResult.CurrentPage)
        {
          Console.WriteLine($"{field.Title,-50} {field.InternalName, -35} {field.TypeDisplayName}");
        }
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
