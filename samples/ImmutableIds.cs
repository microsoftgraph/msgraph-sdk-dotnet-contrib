using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  //public static class ImmutableIds
  //{
  //  public static async Task Run()
  //  {
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
  //    var scopes = new string[] { "https://graph.microsoft.com/Mail.Read" };
  //    IAuthenticationProvider ap = new DeviceCodeProvider(pca, scopes);

  //    ////////////////////////////////////////////////////////////////
  //    //
  //    //  Create a GraphClient with the Logging handler
  //    //
  //    ////////////////////////////////////////////////////////////////

  //    var logger = new StringBuilderHttpMessageLogger();
  //    /*
		//	 *  Could also use the Console if preferred...
		//	 *  
		//	 *  var logger = new ConsoleHttpMessageLogger();
		//	 */

  //    // Configure our client
  //    CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
  //    {
  //      UserAgent = "ImmutableIdsSample"
  //    };

  //    var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, ap);

  //    ///////////////////////////////////////
  //    //
  //    // Setup is complete, run the sample
  //    //
  //    ///////////////////////////////////////

  //    var messages =
  //      await graphServiceClient
  //              .Me
  //              .Messages
  //              .Request()
  //              .Top(1)
  //              .GetAsync();

  //    Console.WriteLine($"ID: {messages.CurrentPage[0].Id}");

  //    Console.WriteLine();

  //    var messagesI =
  //      await graphServiceClient
  //              .Me
  //              .Messages
  //              .Request()
  //              .WithImmutableId()
  //              .Top(1)
  //              .GetAsync();

  //    Console.WriteLine($"ImmutableId: {messagesI.CurrentPage[0].Id}");
  //    Console.WriteLine();

  //    Console.WriteLine("Press enter to show log");
  //    Console.ReadLine();
  //    Console.WriteLine();
  //    Console.WriteLine(logger.GetLog());
  //  }
  //}
}
