using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class SharePointSearch
  {
    private readonly AzureAdSettings azureAdSettings;
    private readonly SharePointSettings sharePointSettings;

    public SharePointSearch(
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
        UserAgent = "SharePointSearchSample"
      };

      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      ///////////////////////////////////////

      var scopes = new string[] { $"https://{sharePointSettings.Hostname}/AllSites.FullControl" };
      var WebUrl = $"https://{sharePointSettings.Hostname}{sharePointSettings.SiteCollectionUrl}";

      var queryText = $"adaptive";
      var propsToSelect = new List<string>() { "Title", "Path", "DocId]" };
      var sortList = new List<SearchQuery.Sort>() { new SearchQuery.Sort("DocId", SearchQuery.SortDirection.Ascending) };

      var query = new SearchQuery(
        queryText: queryText,
        selectProperties: propsToSelect,
        sortList: sortList);

      try
      {
        var results = await graphServiceClient
                        .SharePointAPI(WebUrl)
                        .Search
                        .Request()
                        .WithScopes(scopes)
                        .PostQueryAsync(query);

        var rowCount = results.PrimaryQueryResult.RelevantResults.RowCount;
        var totalRows = results.PrimaryQueryResult.RelevantResults.TotalRows;

        Console.WriteLine($"rowCount: {rowCount}");

        string lastDocId = null;
        foreach (var item in results.PrimaryQueryResult.RelevantResults.Table.Rows)
        {
          Console.WriteLine(item.Cells.FirstOrDefault(c => c.Key == "Path").Value);

          var docId = item.Cells.FirstOrDefault(c => c.Key == "DocId")?.Value;
          if (docId != null)
          {
            lastDocId = docId;
          }
        }

        if (totalRows > rowCount && !string.IsNullOrEmpty(lastDocId))
        {
          var nextPageQuery = new SearchQuery(
            queryText: $"{queryText} indexdocid>{lastDocId}",
            selectProperties: propsToSelect,
            sortList: sortList);

          var page2results = await graphServiceClient
                        .SharePointAPI(WebUrl)
                        .Search
                        .Request()
                        .PostQueryAsync(nextPageQuery);

          foreach (var item in page2results.PrimaryQueryResult.RelevantResults.Table.Rows)
          {
            Console.WriteLine(item.Cells.FirstOrDefault(c => c.Key == "Path").Value);

          }
        }
        Console.WriteLine($"totalRows: {totalRows}");
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
