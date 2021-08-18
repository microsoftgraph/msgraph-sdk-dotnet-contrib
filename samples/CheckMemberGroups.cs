using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public static class CheckMemberGroups
  {
    //public static async Task Run()
    //{
    //  ////////////////////////////////
    //  //
    //  // Azure AD Configuration
    //  //
    //  ////////////////////////////////

    //  AzureAdOptions azureAdOptions = new AzureAdOptions();

    //  var settingsFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
    //  var builder = new ConfigurationBuilder()
    //                      .AddJsonFile(settingsFilename, optional: false);
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
    //    ClientId = "301849c2-7157-47b5-ab96-dc65885735bc", //azureAdOptions.ClientId,
    //    TenantId = "common", // azureAdOptions.TenantId,
    //    RedirectUri = "http://localhost"
    //  };

    //  // Create the public client application (desktop app), with a default redirect URI
    //  var pca = PublicClientApplicationBuilder.CreateWithApplicationOptions(options)
    //      .Build();

    //  // Enable a simple token cache serialiation so that the user does not need to
    //  // re-sign-in each time the application is run
    //  TokenCacheHelper.EnableSerialization(pca.UserTokenCache);

    //  ///////////////////////////////////////////////
    //  //
    //  //  Auth Provider - Delegate in this example
    //  //
    //  ///////////////////////////////////////////////

    //  var accessToken = "eyJ0eXAiOiJKV1QiLCJub25jZSI6Img2bWF0WjNac0VXcFpHNDRXQUpCanBrOGJ5RHBQazUzdExpSDBfdlJ4UHMiLCJhbGciOiJSUzI1NiIsIng1dCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyIsImtpZCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9iMDg5ZDFmMS1lNTI3LTRiOGEtYmE5Ni0wOTQ5MjJhZjZlNDAvIiwiaWF0IjoxNjE3NzEyMDc3LCJuYmYiOjE2MTc3MTIwNzcsImV4cCI6MTYxNzcxNTk3NywiYWNjdCI6MCwiYWNyIjoiMSIsImFjcnMiOlsidXJuOnVzZXI6cmVnaXN0ZXJzZWN1cml0eWluZm8iLCJ1cm46bWljcm9zb2Z0OnJlcTEiLCJ1cm46bWljcm9zb2Z0OnJlcTIiLCJ1cm46bWljcm9zb2Z0OnJlcTMiLCJjMSIsImMyIiwiYzMiLCJjNCIsImM1IiwiYzYiLCJjNyIsImM4IiwiYzkiLCJjMTAiLCJjMTEiLCJjMTIiLCJjMTMiLCJjMTQiLCJjMTUiLCJjMTYiLCJjMTciLCJjMTgiLCJjMTkiLCJjMjAiLCJjMjEiLCJjMjIiLCJjMjMiLCJjMjQiLCJjMjUiXSwiYWlvIjoiRTJaZ1lPZ1B2ZjFTVUwvb2dYZ1VtNitTNS9PVEtScFNndDk1Ni81SlhqRjFMVHhndWhRQSIsImFtciI6WyJwd2QiXSwiYXBwX2Rpc3BsYXluYW1lIjoiR3JhcGggZXhwbG9yZXIgKG9mZmljaWFsIHNpdGUpIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IlNjaGFlZmxlaW4iLCJnaXZlbl9uYW1lIjoiUGF1bCIsImlkdHlwIjoidXNlciIsImlwYWRkciI6IjczLjIwOS4xMDIuMjciLCJuYW1lIjoiUGF1bCBTY2hhZWZsZWluIiwib2lkIjoiODY0NzE4NTgtYWNlZC00ZDM5LWJkNzEtMjhlZTNkMDZiYTk3IiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMwMDAwQURBNzQwNEYiLCJyaCI6IjAuQVNBQThkR0pzQ2ZsaWt1NmxnbEpJcTl1UUxYSWk5NzUyYkZJcUsyM1NOcHlVR1FnQUs4LiIsInNjcCI6IkFwcENhdGFsb2cuUmVhZFdyaXRlLkFsbCBDYWxlbmRhcnMuUmVhZFdyaXRlIENoYXQuUmVhZCBDb250YWN0cy5SZWFkV3JpdGUgRGlyZWN0b3J5LkFjY2Vzc0FzVXNlci5BbGwgRGlyZWN0b3J5LlJlYWRXcml0ZS5BbGwgRmlsZXMuUmVhZFdyaXRlLkFsbCBHcm91cC5SZWFkLkFsbCBHcm91cC5SZWFkV3JpdGUuQWxsIElkZW50aXR5Umlza0V2ZW50LlJlYWQuQWxsIE1haWwuUmVhZFdyaXRlIE5vdGVzLlJlYWRXcml0ZS5BbGwgb3BlbmlkIFBlb3BsZS5SZWFkIHByb2ZpbGUgU2l0ZXMuUmVhZFdyaXRlLkFsbCBUYXNrcy5SZWFkV3JpdGUgVXNlci5SZWFkIFVzZXIuUmVhZEJhc2ljLkFsbCBVc2VyLlJlYWRXcml0ZSBVc2VyLlJlYWRXcml0ZS5BbGwgZW1haWwiLCJzaWduaW5fc3RhdGUiOlsia21zaSJdLCJzdWIiOiJtcHZpemlTSENoYndjT2VvY3FKaUx0dUQ5OFlET1pLRUdGLXhvQXFUb2lRIiwidGVuYW50X3JlZ2lvbl9zY29wZSI6IkVVIiwidGlkIjoiYjA4OWQxZjEtZTUyNy00YjhhLWJhOTYtMDk0OTIyYWY2ZTQwIiwidW5pcXVlX25hbWUiOiJwYXVsQGRldi5hZGRpbjM2NS5jb20iLCJ1cG4iOiJwYXVsQGRldi5hZGRpbjM2NS5jb20iLCJ1dGkiOiJaLVF3YUJ0akJVR29aNzRhSTdsc0FBIiwidmVyIjoiMS4wIiwid2lkcyI6WyJmMjhhMWY1MC1mNmU3LTQ1NzEtODE4Yi02YTEyZjJhZjZiNmMiLCI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6IkIyZllpTk1HR1F6dmdFQy16M0RoSzFSRU1ZMU5KZ0YwWmxyWHBsaHc1UEUifSwieG1zX3RjZHQiOjE0NDAxNzM0OTB9.SknPFrH9KSNaiGoy1r9BJ_vf12EjuU0SWph82enVKE5RHoR5hHsew066zln4L4M7qH3Lu_EPSU9BYfntCKwGTgjssygCdcenFJ_vaY2Y9QWYx2gxkEuPmOdagfU5eoZYM-4Ll1-GCdq4cTH3w6TIEdS4VFvkxSbDNKoE7P_uksmuVhBcvxrB9bDH8kjNQhT46rsBWfQU0wb9ls-QT1IoQStbfB0FmMmMsLRIlZhfpOyp2Rr40s36mO9VkwHhQFcjNBknap7KS5kz4w5laEH-Dm2yQokqvpZ2BqM3W4V8dXtzZ41biedTAWmYGPFiisdzK_1wQq7SAGZhBQMslJyB2g";

    //  // Create an authentication provider to attach the token to requests
    //  IAuthenticationProvider ap = new DelegateAuthenticationProvider((requestMessage) =>
    //  {
    //    requestMessage
    //        .Headers
    //        .Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    //    return Task.CompletedTask;
    //  });

    //  ////////////////////////////////////////////////////////////////
    //  //
    //  //  Create a GraphClient with the Logging handler
    //  //
    //  ////////////////////////////////////////////////////////////////

    //  // Log Http Request/Response
    //  var logger = new StringBuilderHttpMessageLogger();

    //  // Configure our client
    //  CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
    //  {
    //    UserAgent = "CheckMemberGroups"
    //  };

    //  var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, logger, ap);


    //  ///////////////////////////////////////
    //  //
    //  // Setup is complete, run the sample
    //  //
    //  ///////////////////////////////////////

    //  var userId = "86471858-aced-4d39-bd71-28ee3d06ba97";

    //  //.AppendSegmentToRequestUrl("microsoft.graph.directoryRole")
    //  var x = graphServiceClient
    //                  .Users[userId]
    //                  .MemberOf
    //                  .WithODataCast("microsoft.graph.directoryRole")
    //                  .Request()
    //                  .GetAsync();



    //  // Build the batch
    //  var batchRequestContent = new BatchRequestContent();
    //  List<string> batchRequestIds = new List<string>();

    //  var groupsProcessed = 0;
    //  do
    //  {
    //    var pageOfGroupIds = groupIds.Skip(groupsProcessed).Take(20).ToArray();
    //    var requestBody = new DirectoryObjectCheckMemberGroupsRequestBody() { GroupIds = pageOfGroupIds };

    //    //var temp = await graphServiceClient.Users[userId].CheckMemberGroups(pageOfGroupIds).Request().PostAsync();

    //    var checkMbrGroupsRequest = graphServiceClient.Users[userId].CheckMemberGroups(pageOfGroupIds).Request().GetHttpRequestMessage();
    //    checkMbrGroupsRequest.Method = HttpMethod.Post;
    //    checkMbrGroupsRequest.Content = new StringContent(graphServiceClient.HttpProvider.Serializer.SerializeObject(requestBody), Encoding.UTF8, "application/json");

    //    var id = batchRequestContent.AddBatchRequestStep(checkMbrGroupsRequest);
    //    batchRequestIds.Add(id);

    //    groupsProcessed += pageOfGroupIds.Length;
    //  } while (groupsProcessed < groupIds.Count);

    //  List<string> results = new List<string>();
    //  try
    //  {
    //    var returnedResponse = await graphServiceClient.Batch.Request()
    //                                  .PostAsync(batchRequestContent);


    //    foreach (var batchRequestId in batchRequestIds)
    //    {
    //      var batchResults = await returnedResponse.GetResponseByIdAsync<DirectoryObjectCheckMemberGroupsCollectionResponse>(batchRequestId);
    //      results.AddRange(batchResults.Value.CurrentPage);
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    await logger.WriteLine("");
    //    await logger.WriteLine("================== Exception caught ==================");
    //    await logger.WriteLine(ex.ToString());
    //  }

    //  Console.WriteLine(results.Count);

    //  Console.WriteLine("Press enter to show log");
    //  Console.ReadLine();
    //  Console.WriteLine();
    //  var log = logger.GetLog();
    //  Console.WriteLine(log);




    //}


    //private static List<string> groupIds = new List<string>               {
    //         "0053f839-5f1d-4a25-b401-99d6b7f30661",

    //         "00707ede-655a-4e76-b0b9-ce07f1d2c9c9"
    //    ,

    //         "02068c01-e2ae-46ef-befd-64f240846ef2"
    //    ,

    //         "020d9058-c674-40a3-892d-4cffbe461d6a"
    //    ,

    //         "038660bf-5da2-43d7-8fc5-0b0a4b882d49"
    //    ,

    //         "04055fa1-042f-4c4e-b7db-5fcb97922e79"
    //    ,

    //         "0449540f-cb0c-4a4e-85ee-e8d737585f7e"
    //    ,

    //         "046c5dc6-901d-44f2-8ea8-4db08f709616"
    //    ,

    //         "0502576c-4b93-4758-bfcf-dc21567e3651"
    //    ,

    //         "06bbc231-684b-4b15-b2cf-4ca78f3e183e"
    //    ,

    //         "06e7d473-a3c8-48df-af12-7ac4499f226c"
    //    ,

    //         "0808f6d4-4935-4117-8b8a-7c10bb664843"
    //    ,

    //         "0894009b-2d21-4d0b-88ef-25ad91c0f4b6"
    //    ,

    //         "0b0d46ef-83c9-417d-b2b1-050379511f00"
    //    ,

    //         "0ba227e6-1d4a-4628-9df1-6ee654d4f10e"
    //    ,

    //         "0d1d5aaf-c260-4abb-90b1-fce6dc511020"
    //    ,

    //         "0d6e1663-01d4-4f1e-87a4-1849515b5d14"
    //    ,

    //         "0d99494b-f0e2-45fa-9fc0-8ad52cd6deaa"
    //    ,

    //         "0e0f6ce1-d688-44ea-826b-cf5df771881e"
    //    ,

    //         "0e56036a-2d1a-44d5-89c5-f1a98b75f440"
    //    ,

    //         "0f3ff37c-5162-41c1-acf4-35b6f34fd52b"
    //    ,

    //         "10e501cd-ba01-4e2c-9606-96fed434c599"
    //    ,

    //         "11e952bc-a494-4f39-a9c9-fbc17ec8f7d7"
    //    ,

    //         "14475601-924e-4f4e-a7d0-feb20c8c51b6"
    //    ,

    //         "15395061-fe6d-44ae-b2dc-7d4be7e6bbce"
    //    ,

    //         "15ebc995-2f52-4028-8609-12d3248bd026"
    //    ,

    //         "1743a4b8-6490-475f-935b-e4d3c2c7b028"
    //    ,

    //         "1744abd5-9abe-4ec7-87e2-fc71178f1e43"
    //    ,

    //         "19c21a34-1276-4a9a-bc21-e0a2ec6042c4"
    //    ,

    //         "1a6785af-eb31-45be-8c02-92672b6c6f62"
    //    ,

    //         "1b197899-6d4b-421c-84ec-21a1d13eb673"
    //    ,

    //         "1be2612c-7f9c-4380-b8d6-533c05482206"
    //    ,

    //         "1da23ca4-3ab6-4810-ab32-96fdec8ec52e"
    //    ,

    //         "1dc7fb6f-6b58-4753-ad32-830371902237"
    //    ,

    //         "1e64e0c1-bdad-462d-948a-233dae8bd0a8"
    //    ,

    //         "2021beea-3859-442a-ba94-4ee10b3b1f96"
    //    ,

    //         "2418886e-56ef-434a-a86b-be79949b87bf"
    //    ,

    //         "262109b9-7893-40c6-b72c-9304d89bdf37"
    //    ,

    //         "27c587cf-722f-4f7e-9a3b-270fdf3dae8b"
    //    ,

    //         "2ad2c19f-5632-4f3d-8f4a-d4e7a07cb4bd"
    //    ,

    //         "2b12c7c0-07c3-47b3-98eb-cb24001f1b74"
    //    ,

    //         "2b3f3d4d-91e2-4738-a2a2-d01b1c971bdf"
    //    ,

    //         "2c7c1251-d97f-4a0b-ad6c-da0144f81d04"
    //    ,

    //         "2cb2c29a-4e4e-4b59-91cb-d1e52aa5e00c"
    //    ,

    //         "2e63ad22-c777-4b36-b316-6344da8c1614"
    //    ,

    //         "2f8297df-8268-44e7-9a49-85127d42d845"
    //    ,

    //         "2fb9f5b4-7a26-4ca3-a01e-21ceb6b7d77a"
    //    ,

    //         "31dc056e-946d-49e2-8de4-aa640d71f14a"
    //    ,

    //         "333ee191-db67-4622-b01e-15cf18414cfa"
    //    ,

    //         "33dc8d5f-da63-4dde-961e-dca03a2216b8"
    //    ,

    //         "36eb1cb6-fc51-4f6e-b4f5-007196553cef"
    //    ,

    //         "37274f59-0065-4211-ae50-d2257885a2d0"
    //    ,

    //         "373a9c24-63c2-4f4f-b17f-a917b7f86a9d"
    //    ,

    //         "3755db5c-6a03-4e47-b369-b38f915003c0"
    //    ,

    //         "3893a1da-c506-4614-af69-559406527158"
    //    ,

    //         "399b6eca-40dd-427b-accb-26933bcc653f"
    //    ,

    //         "3b764ef8-8e4e-4480-a7bf-d344c4a58a1f"
    //    ,

    //         "3d0b3a83-9bb5-43ae-b6e3-459464f4f226"
    //    ,

    //         "3f2a0aba-0e03-49a0-9f6b-35afb09e1558"
    //    ,

    //         "407bc337-5659-449b-8d45-baa0152f11bf"
    //    ,

    //         "41c3de0f-1d7f-4085-a76e-d7abb7143f5d"
    //    ,

    //         "42c10c76-2b01-4048-8d31-080835f649eb"
    //    ,

    //         "42ebfc87-f517-4a22-a332-b1d6d015a9b8"
    //    ,

    //         "431bb1c9-42e6-430a-a496-3eedabf560d7"
    //    ,

    //         "4506c299-ff8f-41cf-b47a-89fb117e6e9b"
    //    ,

    //         "450cd21a-6b21-4bac-bb70-e4f3dda4df8c"
    //    ,

    //         "45325171-9c48-45d1-b837-3e6579da82cb"
    //    ,

    //         "4861004c-752c-4da6-92b4-b9ec2e4e4d4a"
    //    ,

    //         "48a20cf6-e2ac-469b-abac-f6d80312e71f"
    //    ,

    //         "48d5ef53-6065-4185-949e-159e3c3281fd"
    //    ,

    //         "49abb714-2030-4a47-87d7-920a4d6d2191"
    //    ,

    //         "49c3d3cd-5fb7-46d3-a4f2-e70ddd6b37ab"
    //    ,

    //         "49efe62c-e378-4ef2-9927-94924a299f3c"
    //    ,

    //         "4b31da25-2261-40f7-8f2c-597d80194e1c"
    //    ,

    //         "4bd064bb-76cd-4c9a-99f5-2824c3f4fe26"
    //    ,

    //         "4d15f379-9e07-4762-a357-0be632727f59"
    //    ,

    //         "5244c94c-bce2-4d5b-affb-3c86d19540c9"
    //    ,

    //         "533f6000-e746-40bf-822c-d3ee6d5ae1f8"
    //    ,

    //         "53e1cbb0-dc07-424e-b11f-79a2d4117f6c"
    //    ,

    //         "54048623-8212-4631-8f7e-36a839ed5bcc"
    //    ,

    //         "544e5594-451d-4a52-ad97-8e20a9ebce18"
    //    ,

    //         "54d9400d-c32a-49db-b708-f397819273b7"
    //    ,

    //         "55178de8-035b-4f4d-9978-fe1fc4b331f9"
    //    ,

    //         "55180273-6594-4007-b5a6-d806cd35d293"
    //    ,

    //         "551bad8d-9663-4ff3-b5b3-528a02779618"
    //    ,

    //         "555c13d5-0cff-49d0-9845-70cac3b2fc80"
    //    ,

    //         "5582d3be-9c09-4036-befe-0558a6cff7d5"
    //    ,

    //         "568f7374-bf5f-4354-9a6c-d6b90e117369"
    //    ,

    //         "588de588-3cc6-4975-94bf-af7b960d14dd"
    //    ,

    //         "58c1d7a7-f862-41fd-8bca-de1e3b63fff4"
    //    ,

    //         "59097ed6-fdac-420a-a62e-1d00d9c354cb"
    //    ,

    //         "5b91dbb5-1443-4828-a1bc-78fe10ada24f"
    //    ,

    //         "5c18e36d-7d98-4f33-b2fa-57bd969f10b7"
    //    ,

    //         "5d801b39-aad9-45a9-a026-f5a3fd8d763a"
    //    ,

    //         "5f6d000b-1343-4edc-9c93-2cd1254ba7de"
    //    ,

    //         "60288085-2160-46b4-a1e7-faf855c22ebc"
    //    ,

    //         "6046a93e-33ec-4cc0-a73a-f28b7f76a186"
    //    ,

    //         "6104a6aa-1e3f-4dfa-ab1c-e0bf3622adfd"
    //    ,

    //         "6210712e-1d6f-459d-824d-2cc5bee04010"
    //    ,

    //         "62c1aeda-31e0-4792-b11b-d8ffabb8e4cf"
    //    };

  }
}
