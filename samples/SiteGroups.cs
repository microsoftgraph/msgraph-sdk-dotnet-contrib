using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  //public static class SiteGroups
  //{
  //  public static async Task Run()
  //  {
  //    /////////////////////////////
  //    //
  //    // Programmer configuration
  //    //
  //    /////////////////////////////

  //    var sharepointDomain = "demo.sharepoint.com";
  //    var siteCollectionPath = "/sites/GraphCommunityDemo";

  //    ////////////////////////////////
  //    //
  //    // Azure AD Configuration
  //    //
  //    ////////////////////////////////

  //    AzureAdOptions azureAdOptions = new AzureAdOptions();

  //    var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
  //    var builder = new ConfigurationBuilder()
  //                        .AddJsonFile(settingsFilename, optional: false)
  //                        .AddUserSecrets<Program>();
  //    var config = builder.Build();
  //    config.Bind("AzureAd", azureAdOptions);

  //    /////////////////////////////////////
  //    //
  //    // Client Application Configuration
  //    //
  //    /////////////////////////////////////

  //    var options = new PublicClientApplicationOptions()
  //    {
  //      AadAuthorityAudience = AadAuthorityAudience.AzureAdMyOrg,
  //      AzureCloudInstance = AzureCloudInstance.AzurePublic,
  //      ClientId = azureAdOptions.ClientId,
  //      TenantId = azureAdOptions.TenantId,
  //      RedirectUri = "http://localhost"
  //    };

  //    // Create the public client application (desktop app), with a default redirect URI
  //    var pca = PublicClientApplicationBuilder
  //                .CreateWithApplicationOptions(options)
  //                .Build();

  //    // Enable a simple token cache serialiation so that the user does not need to
  //    // re-sign-in each time the application is run
  //    TokenCacheHelper.EnableSerialization(pca.UserTokenCache);

  //    ///////////////////////////////////////////////
  //    //
  //    //  Auth Provider - Device Code in this sample
  //    //
  //    ///////////////////////////////////////////////

  //    // Create an authentication provider to attach the token to requests
  //    var scopes = new string[] { $"https://{sharepointDomain}/AllSites.FullControl" };
  //    IAuthenticationProvider ap = new DeviceCodeProvider(pca, scopes);


  //    ////////////////////////////////////////////////////////////
  //    //
  //    // Graph Client with Logger and SharePoint service handler
  //    //
  //    ////////////////////////////////////////////////////////////

  //    var logger = new StringBuilderHttpMessageLogger();
  //    /*
		//	 *  Could also use the Console if preferred...
		//	 *  
		//	 *  var logger = new ConsoleHttpMessageLogger();
		//	 */

  //    // Configure our client
  //    CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
  //    {
  //      UserAgent = "SiteGroupsSample"
  //    };
  //    var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, ap);

  //    ///////////////////////////////////////
  //    //
  //    // Setup is complete, run the sample
  //    //
  //    ///////////////////////////////////////

  //    var WebUrl = $"https://{sharepointDomain}{siteCollectionPath}";

  //    var groups = await graphServiceClient
  //                    .SharePointAPI(WebUrl)
  //                    .Web
  //                    .SiteGroups
  //                    .Request()
  //                    .Expand(g => g.Users)
  //                    .Expand("Owner")
  //                    .GetAsync();


  //    foreach (var group in groups)
  //    {
  //      Console.WriteLine(group.Title);
  //      foreach (var user in group.Users)
  //      {
  //        Console.WriteLine($"  {user.LoginName}");
  //      }
  //    }

  //    Console.WriteLine("Press enter to show log");
  //    Console.ReadLine();
  //    Console.WriteLine();
  //    var log = logger.GetLog();
  //    Console.WriteLine(log);
  //  }
  //}

}
