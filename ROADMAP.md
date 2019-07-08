# Request builders in the Graph.Community extension library

This document lists the request builders that are included in the library

## Endpoint: Office 365 SharePoint

The Office 365 SharePoint endpoint has request builders accessible using the `SharePointAPI` method. The reference page for the SharePoint  extensions is https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/sharepoint-net-server-csom-jsom-and-rest-api-index and https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/ee536622(v=office.14)

### Site Scripts and Site Designs
The following operations use the `_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScripts` url.

| Operation                      | Request Builder    | Method      | Released version |
|--------------------------------|--------------------|-------------|------------------|
| CreateSiteScript               | `.SiteScripts`     | CreateAsync | 1.16             |
| GetSiteScripts                 | `.SiteScripts`     | GetAsync    | 1.16             |
| GetSiteScriptFromList          | `.SiteScripts`     |             |                  |
| GetSiteScriptMetadata          | `.SiteScripts[id]` |GetAsync     | 1.16             |
| UpdateSiteScripts              | `.SiteScripts`     |             |                  |
| DeleteSiteScripts              | `.SiteScripts`     |             |                  |
|                                |                    |             |                  |
| CreateSiteDesign               | `.SiteDesigns`     |CreateAsync  | 1.16             |
| ApplySiteDesign                | `.SiteDesigns`     |ApplyAsync   | 1.16             |
| AddSiteDesignTaskToCurrentWeb  | `.SiteDesigns`     |             |                  |
| GetSiteDesigns                 | `.SiteDesigns`     |GetAsync     | 1.16             |
| GetSiteDesignMetadata          | `.SiteDesigns[id]` |GetAsync     | 1.16             |
| UpdateSiteDesign               | `.SiteDesigns`     |             |                  |
| DeleteSiteDesign               | `.SiteDesigns`     |             |                  |
|                                |                    |             |                  |
| GetSiteDesignRights            |                    |             |                  |
| GrantSiteDesignRights          |                    |             |                  |
| RevokeSiteDesignRights         |                    |             |                  |

### Change log
The following operations allow for reading the SharePoint change log. The information operations will return the [current ChangeToken](src/Models/ChangeLog/ChangeToken.cs) for the object.

| Operation                              | Request Builder                                       | Method          | Released version|
|----------------------------------------|-------------------------------------------------------|-----------------|-----------------|
| [Site Information](src/Models/Site.cs) | `.Site`                                               | GetAsync        |1.16             |
| Site Changes                           | `.Site`                                               | GetChangesAsync |1.16             |
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

### SharePoint Search

[SharePoint Search REST API](https://docs.microsoft.com/en-us/sharepoint/dev/general-development/sharepoint-search-rest-api-overview)

### SharePoint User Profile

[SharePoint User Profile REST reference](https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-rest-reference/dn790354(v=office.15))

### Hub Sites

[Hub Sites REST API](https://docs.microsoft.com/en-us/sharepoint/dev/features/hub-site/hub-site-rest-api)

### SPFx ALM

[ALM API for SPFx Add-Ins](https://docs.microsoft.com/en-us/sharepoint/dev/apis/alm-api-for-spfx-add-ins)
