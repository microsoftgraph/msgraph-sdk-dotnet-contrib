using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  //public static class CreateTeam
  //{
  //  public static async Task Run()
  //  {

  //    /////////////////////////////////////
  //    //
  //    // Client Application Configuration
  //    //
  //    /////////////////////////////////////

  //    var options = new PublicClientApplicationOptions()
  //    {
  //      AadAuthorityAudience = AadAuthorityAudience.AzureAdMyOrg,
  //      AzureCloudInstance = AzureCloudInstance.AzurePublic,
  //      ClientId = "301849c2-7157-47b5-ab96-dc65885735bc",
  //      TenantId = "b089d1f1-e527-4b8a-ba96-094922af6e40",
  //      RedirectUri = "http://localhost"
  //    };

  //    // Create the public client application (desktop app), with a default redirect URI
  //    var pca = PublicClientApplicationBuilder.CreateWithApplicationOptions(options)
  //        .Build();

  //    // Enable a simple token cache serialiation so that the user does not need to
  //    // re-sign-in each time the application is run
  //    TokenCacheHelper.EnableSerialization(pca.UserTokenCache);

  //    ///////////////////////////////////////////////
  //    //
  //    //  Auth Provider - Interactive in this sample
  //    //
  //    ///////////////////////////////////////////////

  //    // Use the system browser to login
  //    //  https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core#how-to-use-the-system-browser-ie-the-default-browser-of-the-os

  //    // Create an authentication provider to attach the token to requests
  //    IAuthenticationProvider ap = new InteractiveAuthenticationProvider(pca);


  //    ////////////////////////////////////////////////////////////////
  //    //
  //    //  Create a GraphClient with the Logging handler
  //    //
  //    ////////////////////////////////////////////////////////////////

  //    // Log Http Request/Response
  //    var logger = new StringBuilderHttpMessageLogger();

  //    // Configure our client
  //    CommunityGraphClientOptions clientOptions = new("AddIn365", "SaturnV.Research", "0.0.1", true);
  //    var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, ap);


  //    ///////////////////////////////////////
  //    //
  //    // Setup is complete, run the sample
  //    //
  //    ///////////////////////////////////////

  //    try
  //    {
  //      var teamsApps = await graphServiceClient
  //                                .AppCatalogs
  //                                .TeamsApps
  //                                .Request()
  //                                .Filter("externalId eq 'a77ec467-4ed4-419f-9b00-e027d49e69de'")
  //                                .GetAsync();
  //      var botTeamsApp = teamsApps.FirstOrDefault();

  //      var displayName = $"paul_v1_{DateTime.Now:MMdd_hhmm}";
  //      var description = "desc";

  //      //var createTeamRequest = new CreateTeamRequest
  //      //{
  //      //	DisplayName = displayName,
  //      //	Description = description,
  //      //	InstalledApps = new List<InstalledApp>() { new InstalledApp() { TeamsAppId = botTeamsApp.Id } }
  //      //};

  //      var additionalData = new Dictionary<string, object>()
  //      {
  //        {
  //          "template@odata.bind",
  //          "https://graph.microsoft.com/v1.0/teamsTemplates('standard')"
  //        },
  //        {
  //          "installedApps",
  //          new List<InstalledApp>()
  //        }
  //      };

  //      (additionalData["installedApps"] as List<InstalledApp>).Add(new InstalledApp { TeamsAppId = botTeamsApp.Id });


  //      var team = new Team
  //      {
  //        DisplayName = displayName,
  //        Description = description,
  //        AdditionalData = additionalData
  //      };


  //      var t = await graphServiceClient.Teams.Request().AddAsync(team);

  //      Console.WriteLine(t.DisplayName);

  //    }
  //    catch (Exception ex)
  //    {
  //      await logger.WriteLine("");
  //      await logger.WriteLine("================== Exception caught ==================");
  //      await logger.WriteLine(ex.ToString());
  //    }


  //    Console.WriteLine("Press enter to show log");
  //    Console.ReadLine();
  //    Console.WriteLine();
  //    var log = logger.GetLog();
  //    Console.WriteLine(log);
  //  }
  //}

  public class InstalledApp
  {
    private string teamsAppId;
    public string TeamsAppId
    {
      get
      {
        return $"https://graph.microsoft.com/v1.0/appCatalogs/teamsApps('{teamsAppId}')";
      }
      set
      {
        teamsAppId = value;
      }
    }
  }

}
