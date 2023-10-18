using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class TeamsTab
  {
    private readonly AzureAdSettings azureAdSettings;

    public TeamsTab(
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
        UserAgent = "TeamsTab"
      };

      var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, credential);

      ///////////////////////////////////////
      //
      // Setup is complete, run the sample
      //
      ///////////////////////////////////////

      var teamId = "4fe37106-1d3a-465b-b178-9c39d6aeb343";
      var channelId = "19:t-MikaBv26FK2VZnqLTQmAtFvoZ4_fCKOKX7S2dXT_k1@thread.tacv2";

      var scopes = new string[] { "TeamsTab.Create" };

      var newChannelTabRequest = new CreateChannelTabRequest()
      {
        DisplayName = $"GCTest:{DateTime.Now.ToShortTimeString()}",
        TeamsAppId = "2a527703-1f6f-4559-a332-d8a7d288cd88",
        Configuration = new Microsoft.Graph.TeamsTabConfiguration
        {
          ContentUrl = "https://devaddin365.sharepoint.com/sites/tabstest/_layouts/15/teamslogon.aspx?spfx=true&dest=https%3A%2F%2Fdevaddin365.sharepoint.com%2Fsites%2FTabstest%2FSitePages%2FHome.aspx",
          RemoveUrl = null,
          WebsiteUrl = "https://devaddin365.sharepoint.com/sites/Tabstest/SitePages/Home.aspx"
        }
      };


      var newTabRequest = newChannelTabRequest.ToTeamsTab();


      var newTab = await graphServiceClient.Teams[teamId]
                          .Channels[channelId]
                          .Tabs
                          .Request()
                          .WithScopes(scopes)
                          .AddAsync(newTabRequest);

      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine(newTab.Id);

      Console.WriteLine();
      Console.WriteLine("Press enter to show log");
      Console.ReadLine();
      Console.WriteLine();
      var log = logger.GetLog();
      Console.WriteLine(log);

    }
  }


  public class CreateChannelTabRequest
  {
    public string DisplayName { get; set; }

    private string teamsAppId;
    public string TeamsAppId
    {
      get
      {
        return $"https://graph.microsoft.com/v1.0/appCatalogs/teamsApps/{teamsAppId}";
      }
      set
      {
        teamsAppId = value;
      }
    }

    public Microsoft.Graph.TeamsTabConfiguration Configuration { get; set; }

    public Microsoft.Graph.TeamsTab ToTeamsTab()
    {
      if (this.TeamsAppId == "com.microsoft.teamspace.tab.web")
      {
        this.Configuration.EntityId = null;
        this.Configuration.RemoveUrl = null;
      }
      else if (this.TeamsAppId == "com.microsoftstream.embed.skypeteamstab")
      {
        this.Configuration.EntityId = null;
        this.Configuration.RemoveUrl = null;
      }
      else if (this.TeamsAppId == "81fef3a6-72aa-4648-a763-de824aeafb7d")
      {
        this.Configuration.RemoveUrl = null;
        this.Configuration.WebsiteUrl = "https://forms.office.com";
      }
      else if (this.TeamsAppId == "com.microsoft.teamspace.tab.files.sharepoint")
      {
        this.Configuration.EntityId = "";
        this.Configuration.RemoveUrl = null;
        this.Configuration.WebsiteUrl = null;
      }

      var teamsTab = new Microsoft.Graph.TeamsTab()
      {
        DisplayName = this.DisplayName,
        ODataBind = this.TeamsAppId,
        Configuration = this.Configuration
      };

      return teamsTab;
    }
  }

}
