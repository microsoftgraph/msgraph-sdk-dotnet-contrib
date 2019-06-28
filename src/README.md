# Graph SDK Community Extensions (Graph.Community)

The Graph extension library is a community effort to unblock developers building on .Net Standard who need to call SharePoint as part of their Microsoft 365 tenant.

The request builders in this library will target the `_api` endpoint of the specified SharePoint site, using an Azure AD Application registration.

## Getting Started

Follow the steps as outlined in the Microsoft.Graph SDK repo: https://github.com/microsoftgraph/msgraph-sdk-dotnet-core.

Once a GraphServiceClient is instantiated, an extension method provides access to the SharePoint REST endpoint. This `SharePointAPI` extension method requires an absolute URL to the SharePoint site collection that is the target of the call. Subsequent methods of the fluent API are used to address the [feature area of the REST API](https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/determine-sharepoint-rest-service-endpoint-uris).

### Example 1
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

### Example 2
Statements:

```csharp
var query = new {
  query = new {
    Add = true,
    DeleteObject = true,
    SystemUpdate = false,
    Update = true,
    ChangeTokenStart = null,
    ChangeTokenEnd = null
  }
};
gsc.SharePointAPI('https://mock.sharepoint.com/sites/mockSite')
     .Site
     .Changes()
     .Request()
     .GetAsync(query);
```

Request:

`POST https://mock.sharepoint.com/sites/mockSite/_api/Site/GetChanges`
