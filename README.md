# Graph SDK Community Extensions (Graph.Community)

The Graph extension library is a community effort to unblock developers building on .Net Standard who need to call endpoints that are not part of the Microsoft Graph.

## Build Status

|Branch|Status|
|------|------|
|master|![Build Status](https://schaeflein.visualstudio.com/Graph.Community/_apis/build/status/microsoftgraph.msgraph-sdk-dotnet-contrib?branchName=master)|
|prerelease|![Build Status](https://schaeflein.visualstudio.com/Graph.Community/_apis/build/status/microsoftgraph.msgraph-sdk-dotnet-contrib?branchName=prerelease)|
|dev|![Build Status](https://schaeflein.visualstudio.com/Graph.Community/_apis/build/status/microsoftgraph.msgraph-sdk-dotnet-contrib?branchName=dev)|
|||
|NuGet|![NuGet package](https://buildstats.info/nuget/Graph.Community)|
|NuGet|![NuGet package](https://buildstats.info/nuget/Graph.Community?includePreReleases=true)|
## Documentation

This community library contains requests and models that extend the Microsoft Graph SDK. Please review the [Roadmap](./ROADMAP.md) for an index of requests that are included and on the roadmap. 

If there is an endpoint node for which you would like a request, please submit an issue to initiate a conversation. This will help reduce wasted effort.

## Getting Started

Follow the steps as outlined in the Microsoft.Graph SDK repo: https://github.com/microsoftgraph/msgraph-sdk-dotnet-core.

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