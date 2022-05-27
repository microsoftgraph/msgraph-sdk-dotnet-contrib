using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class SitePages
  {
    private readonly AzureAdSettings azureAdSettings;
    private readonly SharePointSettings sharePointSettings;

    public SitePages(
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
        string firstPageName = default;
        List<SitePage> pages = new List<SitePage>();

        bool iteratorItemCallback(SitePage p)    // equivalent to Func<SitePageListItem, bool> iteratorItemCallback = (p) => {}
        {
          if (string.IsNullOrEmpty(firstPageName))
          {
            firstPageName = p.FileName;
          }
          Console.WriteLine($"{p.FileName,-50} ({p.PromotedState})");
          return true;
        }

        IBaseRequest iteratorRequestConfigurator(IBaseRequest request)
        {
          return request.WithScopes(scopes);
        }

        var pagesResult = await graphServiceClient
                          .SharePointAPI(WebUrl)
                          .SitePages
                          .Request()
                          .WithScopes(scopes)
                          .GetAsync();

        var pageIterator = PageIterator<SitePage>.CreatePageIterator(graphServiceClient, pagesResult, iteratorItemCallback, iteratorRequestConfigurator);
        await pageIterator.IterateAsync();

        if (!string.IsNullOrEmpty(firstPageName))
        {
          Console.WriteLine("Press enter to show first page");
          Console.ReadLine();

          var page = await graphServiceClient
                              .SharePointAPI(WebUrl)
                              .SitePages[firstPageName]
                              .Request()
                              .WithScopes(scopes)
                              .GetAsync();

          Console.WriteLine($"{page.Title} - {page.Id} - {page.UniqueId}");

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
