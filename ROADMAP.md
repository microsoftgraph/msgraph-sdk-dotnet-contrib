# Request builders in the Graph.Community extension library

This document lists the request builders that are included in the library or on the 

## Endpoint: Office 365 SharePoint

The Office 365 SharePoint endpoint has request builders accessible using the `SharePointAPI` method. 

### Site Scripts and Site Designs
The following operations use the `_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScripts` url.

|Operation|Request Builder|Method|Released version|
|-|-|-|-|-|
|CreateSiteScript |`.SiteScripts`|CreateAsync|1.16|
|GetSiteScripts|`.SiteScripts`|GetAsync|1.16|
|GetSiteScriptFromList|`.SiteScripts`|||
|GetSiteScriptMetadata|`.SiteScripts[id]`|GetAsync|1.16|
|UpdateSiteScripts|`.SiteScripts`|||
|DeleteSiteScripts|`.SiteScripts`|||
||
|CreateSiteDesign|`.SiteDesigns`|CreateAsync|1.16|
|ApplySiteDesign|`.SiteDesigns`|ApplyAsync|1.16|
|AddSiteDesignTaskToCurrentWeb|`.SiteDesigns`|||
|GetSiteDesigns|`.SiteDesigns`|GetAsync|1.16|
|GetSiteDesignMetadata|`.SiteDesigns[id]`|GetAsync|1.16|
|UpdateSiteDesign|`.SiteDesigns`|||
|DeleteSiteDesign|`.SiteDesigns`|||
|GetSiteDesignRights||||
|GrantSiteDesignRights||||
|RevokeSiteDesignRights||||

### Change log
The following operations allow for reading the SharePoint change log. The information operations will return the [current ChangeToken](src/Models/ChangeLog/ChangeToken.cs) for the object.

|Operation|Request Builder|Method|Released version|
|-|-|-|-|-|
|[Site Information](src/Models/Site.cs)|`.Site`|GetAsync|1.16|
|Site Changes|`.Site`|GetChangesAsync|1.16|
|[Web Information](src/Models/Web.cs)|`.Web`|GetAsync|1.16|
|Web Changes|`.Web`|GetChangesAsync|1.16|
|[List Information](src/Models/List.cs)|`.Web.Lists[Guid id]` <br/>`.Web.Lists[string title]`|GetAsync|1.16|
|List Changes|`.Web.Lists`|GetChangesAsync|1.16|
