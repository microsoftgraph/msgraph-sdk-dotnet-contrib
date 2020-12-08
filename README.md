# Graph SDK Community Extensions (Graph.Community)

The Graph extension library is a community effort to unblock developers building on .Net Standard who need to call endpoints that are not part of the Microsoft Graph.

![Build Status](https://schaeflein.visualstudio.com/Graph.Community/_apis/build/status/microsoftgraph.msgraph-sdk-dotnet-contrib?branchName=master)
![NuGet package](https://buildstats.info/nuget/Graph.Community)

## Documentation

This community library contains requests and models that extend the Microsoft Graph SDK. Please review the [Roadmap](./docs/ROADMAP.md) for an index of requests that are included and on the roadmap. 

If there is an endpoint node for which you would like a request, please submit an issue to initiate a conversation. This will help reduce wasted effort.

## Breaking change in v3.18

The `SPUser` class returned from the `Web.SiteUsers` request has been renamed to **`User`**. This aligns with the OData.type property returned from the service.

## Getting Started

Starting with v3.21, the library contains middleware (a delegating handler) that will transform errors from SharePoint Online into a ServiceException. This allows consuming code to standardize error handling.

Also included in v3.21 is a client factory class (`CommunityGraphClientFactory`) that provides method to setup this SharePoint Service middleware.

```csharp
IAuthenticationProvider ap = new InteractiveAuthenticationProvider(pca, scopes);

CommunityGraphClientOptions clientOptions = new CommunityGraphClientOptions()
{
  UserAgent = "ExtendedCapabilitiesSample"
};

var graphServiceClient = CommunityGraphClientFactory.Create(clientOptions, ap);
```

A complete implementation is included in the [Diagnostic sample](samples/Diagnostics.cs).

The `CommunityGraphClientOptions` provides for specifing information to [decorate SharePoint REST traffic to help mitigate throttling](https://docs.microsoft.com/en-us/sharepoint/dev/general-development/how-to-avoid-getting-throttled-or-blocked-in-sharepoint-online#how-to-decorate-your-http-traffic-to-avoid-throttling).

Once a GraphServiceClient is instantiated, an extension method provides access to the SharePoint REST endpoint. This `SharePointAPI` extension method requires an absolute URL to the SharePoint site collection that is the target of the call. Subsequent methods of the fluent API are used to address the [feature area of the REST API](https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/determine-sharepoint-rest-service-endpoint-uris).

### Example
Statements:

```csharp
gsc.SharePointAPI('https://mock.sharepoint.com/sites/mockSite')
     .SiteDesigns
     .Request()
     .GetAsync()
```

Request:

```
GET https://mock.sharepoint.com/sites/mockSite/_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns`
```

## Versioning

The version number intentionaly aligned with the version of the Microsoft.Graph package. 


|Version Component|Notes|
|-|-|
|Major|Aligned with Microsoft.Graph|
|Minor|Aligned with Microsoft.Graph|
|Patch|Incremented as requests/models are added to Graph.Community|
|Suffix|Release/build type|

Version suffixes (`#` indicates a sequence number that is reset for each major/minor):
- `-CI-#` Continuous Integration release built from **dev** branch 
- `-preview#` Preview release, built from **prerelease** branch
- No suffix is release build, from **master** branch
