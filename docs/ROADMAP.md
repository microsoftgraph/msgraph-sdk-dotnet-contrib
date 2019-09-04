# Request builders in the Graph.Community extension library

This document lists the request builders that are included in the library

## Endpoint: Microsoft Graph

The extension library contains extensions to the builders shipped in the SDK. These extensions are likely temporary, as they will be removed as the functionality is added to the SDK. 

### Immutable IDs for Outlook

Outlook items (messages, events, contacts, tasks) have an interesting behavior that you've probably either never noticed or has caused you significant frustration: their IDs change. It doesn't happen often, only if the item is moved, but it can cause real problems for apps that store IDs offline for later use. Immutable identifiers enables your application to obtain an ID that does not change for the lifetime of the item.
https://docs.microsoft.com/en-us/graph/outlook-immutable-id

| Operation                      | Request Builder    | Method          | Released version |
|--------------------------------|--------------------|-----------------|------------------|
| All Get operations for Outlook | `IBaseRequest`    | WithImmutableId | 1.16.1           |

## Endpoint: Office 365 SharePoint

The Office 365 SharePoint endpoint has request builders accessible using the `SharePointAPI` method. The reference page for the SharePoint  extensions is https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/sharepoint-net-server-csom-jsom-and-rest-api-index and https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/ee536622(v=office.14)

### Site Scripts and Site Designs
The following operations use the `_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScripts` url.

| Operation                      | Request Builder    | Method      | Released version |
|--------------------------------|--------------------|-------------|------------------|
| CreateSiteScript               | `.SiteScripts`     | CreateAsync | 1.16             |
| GetSiteScripts                 | `.SiteScripts`     | GetAsync    | 1.16             |
| GetSiteScriptFromList          |                    |             |                  |
| GetSiteScriptMetadata          | `.SiteScripts[id]` |GetAsync     | 1.16             |
| UpdateSiteScripts              |                    |             |                  |
| DeleteSiteScripts              |                    |             |                  |
|                                |                    |             |                  |
| CreateSiteDesign               | `.SiteDesigns`     |CreateAsync  | 1.16             |
| ApplySiteDesign                | `.SiteDesigns`     |ApplyAsync   | 1.16             |
| AddSiteDesignTaskToCurrentWeb  |                    |             |                  |
| GetSiteDesigns                 | `.SiteDesigns`     |GetAsync     | 1.16             |
| GetSiteDesignMetadata          | `.SiteDesigns[id]` |GetAsync     | 1.16             |
| UpdateSiteDesign               |                    |             |                  |
| DeleteSiteDesign               |                    |             |                  |
|                                |                    |             |                  |
| GetSiteDesignRights            |                    |             |                  |
| GrantSiteDesignRights          |                    |             |                  |
| RevokeSiteDesignRights         |                    |             |                  |

### Change log
The following operations allow for reading the SharePoint change log. The information operations will return the [current ChangeToken](src/Models/ChangeLog/ChangeToken.cs) for the object.

| Operation                              | Request Builder                                       | Method          | Released version|
|----------------------------------------|-------------------------------------------------------|-----------------|-----------------|
| [Web Information](src/Models/Web.cs)   | `.Web`                                                | GetAsync        |1.16             |
| Web Changes                            | `.Web`                                                | GetChangesAsync |1.16             |
| [List Information](src/Models/List.cs) | `.Web.Lists[Guid id]` <br/>`.Web.Lists[string title]` | GetAsync        |1.16             |
| List Changes                           | `.Web.Lists`                                          | GetChangesAsync |1.16             |

### Navigation
The following operations allow for reading the Web [navigation properties](https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/ee544902%28v%3doffice.14%29).

| Operation                   | Request Builder                                                      | Method      | Released version |
|-----------------------------|----------------------------------------------------------------------|-------------|------------------|
| GetNavigationNodeCollection | `.Web.Navigation.QuickLaunch`<br/>`.Web.Navigation.TopNavigationBar` | GetAsync    | 1.16             |
| AddNavigationNode           | `.Web.Navigation.QuickLaunch`<br/>`.Web.Navigation.TopNavigationBar` | AddAsync    | 1.16             |
| GetNavigationNode           | `.Web.Navigation[int id]`                                            | GetAsync    | 1.16             |
| UpdateNavigationNode        | `.Web.Navigation[int id]`                                            | UpdateAsync | 1.16             |

### SharePoint SiteUsers
The following operations enable the use of Person fields in SharePoint Lists using Microsoft Graph. Calling EnsureUser() will return the LookupId needed to set a Person field in a SharePoint list item.

| Operation    | Request Builder   | Method          | Released version |
|--------------|-------------------|-----------------|------------------|
| EnsureUser   | `.Web`            | EnsureUserAsync | 1.17.1           |
| GetSiteUsers | `.Web.SiteUsers` | GetAsync        | 1.17.1           |


### SharePoint Search

Interested in [SharePoint Search REST API](https://docs.microsoft.com/en-us/sharepoint/dev/general-development/sharepoint-search-rest-api-overview)? [Open an issue](https://github.com/microsoftgraph/msgraph-sdk-dotnet-contrib/issues/new)

### SharePoint User Profile

Interested in [SharePoint User Profile REST reference](https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-rest-reference/dn790354(v=office.15))? [Open an issue](https://github.com/microsoftgraph/msgraph-sdk-dotnet-contrib/issues/new)

### Hub Sites

Interested in [Hub Sites REST API](https://docs.microsoft.com/en-us/sharepoint/dev/features/hub-site/hub-site-rest-api)? [Open an issue](https://github.com/microsoftgraph/msgraph-sdk-dotnet-contrib/issues/new)

