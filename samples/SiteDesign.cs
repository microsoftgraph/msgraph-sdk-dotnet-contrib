using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public static class SiteDesign
  {
    //public static async Task Run()
    //{
    //  /////////////////////////////
    //  //
    //  // Programmer configuration
    //  //
    //  /////////////////////////////

    //  var sharepointDomain = "demo.sharepoint.com";
    //  var siteCollectionPath = "/sites/SiteDesignTest";

    //  ////////////////////////////////
    //  //
    //  // Azure AD Configuration
    //  //
    //  ////////////////////////////////

    //  AzureAdOptions azureAdOptions = new AzureAdOptions();

    //  var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
    //  var builder = new ConfigurationBuilder()
    //                      .AddJsonFile(settingsFilename, optional: false)
    //                      .AddUserSecrets<Program>();
    //  var config = builder.Build();
    //  config.Bind("AzureAd", azureAdOptions);

    //  /////////////////////////////////////
    //  //
    //  // Client Application Configuration
    //  //
    //  /////////////////////////////////////

    //  var options = new PublicClientApplicationOptions()
    //  {
    //    AadAuthorityAudience = AadAuthorityAudience.AzureAdMyOrg,
    //    AzureCloudInstance = AzureCloudInstance.AzurePublic,
    //    ClientId = azureAdOptions.ClientId,
    //    TenantId = azureAdOptions.TenantId,
    //    RedirectUri = "http://localhost"
    //  };

    //  // Create the public client application (desktop app), with a default redirect URI
    //  var pca = PublicClientApplicationBuilder
    //              .CreateWithApplicationOptions(options)
    //              .Build();

    //  // Enable a simple token cache serialiation so that the user does not need to
    //  // re-sign-in each time the application is run
    //  TokenCacheHelper.EnableSerialization(pca.UserTokenCache);


    //  InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions()
    //  {
    //    ClientId = azureAdOptions.ClientId
    //  };

    //  InteractiveBrowserCredential interactiveBrowserCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);

    //  GraphServiceClient graphClient = new GraphServiceClient(myBrowserCredential, scopes); // you can pass the TokenCredential directly to the GraphServiceClient









    //  ///////////////////////////////////////////////
    //  //
    //  //  Auth Provider - Device Code in this sample
    //  //
    //  ///////////////////////////////////////////////

    //  // Create an authentication provider to attach the token to requests
    //  var scopes = new string[] { $"https://{sharepointDomain}/AllSites.FullControl" };
    //  IAuthenticationProvider ap = new DeviceCodeProvider(pca, scopes);

    //  ////////////////////////////////////////////////////////////
    //  //
    //  // Graph Client with Logger and SharePoint service handler
    //  //
    //  ////////////////////////////////////////////////////////////

    //  var logger = new StringBuilderHttpMessageLogger();
    //  /*
			 //*  Could also use the Console if preferred...
			 //*  
			 //*  var logger = new ConsoleHttpMessageLogger();
			 //*/

    //  // Configure our client
    //  CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
    //  {
    //    UserAgent = "SiteDesignSample"
    //  };
    //  var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, ap);

    //  ///////////////////////////////////////
    //  //
    //  // Setup is complete, run the sample
    //  //
    //  ///////////////////////////////////////

    //  var WebUrl = $"https://{sharepointDomain}{siteCollectionPath}";

    //  var siteScript = new SiteScriptMetadata()
    //  {
    //    Title = "Green Theme",
    //    Description = "Apply the Green Theme",
    //    Content = "{\"$schema\": \"schema.json\",\"actions\": [{\"verb\": \"applyTheme\",\"themeName\": \"Green\"}],\"bindata\": { },\"version\": 1}",
    //  };

    //  var createdScript = await graphServiceClient
    //                              .SharePointAPI(WebUrl)
    //                              .SiteScripts
    //                              .Request()
    //                              .CreateAsync(siteScript);

    //  var siteDesign = new SiteDesignMetadata()
    //  {
    //    Title = "Green Theme",
    //    Description = "Apply the Green theme",
    //    SiteScriptIds = new System.Collections.Generic.List<Guid>() { new Guid(createdScript.Id) },
    //    WebTemplate = "64" // 64 = Team Site, 68 = Communication Site, 1 = Groupless Team Site
    //  };

    //  var createdDesign = await graphServiceClient
    //                              .SharePointAPI(WebUrl)
    //                              .SiteDesigns
    //                              .Request()
    //                              .CreateAsync(siteDesign);

    //  var applySiteDesignRequest = new ApplySiteDesignRequest
    //  {
    //    SiteDesignId = createdDesign.Id,
    //    WebUrl = WebUrl
    //  };

    //  var applySiteDesignResponse = await graphServiceClient
    //                                        .SharePointAPI(WebUrl)
    //                                        .SiteDesigns.Request()
    //                                        .ApplyAsync(applySiteDesignRequest);

    //  foreach (var outcome in applySiteDesignResponse.CurrentPage)
    //  {
    //    Console.WriteLine(outcome.OutcomeText);
    //  }


    //  Console.WriteLine("Press enter to show log");
    //  Console.ReadLine();
    //  Console.WriteLine();
    //  var log = logger.GetLog();
    //  Console.WriteLine(log);
    //}
  }
}
