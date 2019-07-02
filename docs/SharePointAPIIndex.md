# SharePoint API index  

This document lists the SharePoint API index with designations for the object's coverage in the official graph API or inclusion in `graspex`.

    Legend:
    ✔ = In graph
    ❌ = Not interested
    ❔  =  Needs investigation
|Graph|API|SP.Object/Enumeration|REST Endpoint|
|--|--|--|--|
|❔|AttachmentCollection|SP.AttachmentCollection|…/_api/web/lists('<list id>')/items(<item id>)/attachmentfiles|
|❔|ChangeCollection|SP.ChangeCollection object|…/_api/web/getchanges(changequery)|
|✔|ContentType |SP.ContentType object|…/_api/web/contenttypes('<content type id>')|
|✔|ContentTypeCollection|SP.ContentTypeCollection object|…/_api/web/contenttypes|
|❌|EventReceiverDefinition|SP.EventReceiverDefinition object|…/_api/web/eventreceivers|
|❌|EventReceiverDefinitionCollection|SP.EventReceiverDefinitionCollection object|…/_api/web/eventreceivers(eventreceiverid)|
|❌|Feature|SP.Feature object|…/_api/web/features(featureid)|
|❌|FeatureCollection|SP.FeatureCollection object|…/_api/web/features|
|✔|Field|SP.Field object|[…/_api/web/fields('')](https://msdn.microsoft.com/en-us/library/dn600182.aspx#bk_Field)|
|❌|File|SP.File object|…/_api/web/getfilebyserverrelativeurl('//')|
|❌|FileCollection|SP.FieldCollection object|…/_api/web/getfolderbyserverrelativeurl('/')/files|
|✔|Folder|SP.Folder object|…/_api/web/getfolderbyserverrelativeurl('/')|
|❌|Form|SP.Form object|…/_api/web/lists(guid'<list id>')/forms('<form id>')|
|❌|Group|SP.Group object|…/_api/web/sitegroups()|
|❌|GroupCollection|SP.GroupCollection object|…/_api/web/sitegroups|
|✔|List|SP.List object|…/_api/web/lists(guid'')|
|✔|ListCollection|SP.ListCollection object|…/_api/web/lists|
|✔|ListItem|SP.ListItem object|…/_api/web/lists(guid'')/items()|
|✔|ListItemCollection |SP.ListItemCollection object|…/_api/web/lists(guid'')/items|
|❔|Navigation|SP.Navigation object|…/_api/web/navigation|
|❔|RecycleBinItem |SP.RecycleBinItem object|…/_api/web/RecycleBin(recyclebinitemid)|
|❔|RecycleBinItemCollection|SP.RecycleBinItemCollection object|…/_api/web/RecycleBin|
|❌|RegionalSettings|SP.RegionalSettings object|…/_api/web/RegionalSettings|
|❌|RoleAssignment |SP.RoleAssignment object|…/_api/web/roleassignments()|
|❌|RoleAssignmentCollection|SP.RoleAssignmentCollection object|…/_api/web/roleassignments|
|❌|RoleDefinition|SP.RoleDefinition object|…/_api/web/roledefinitions()|
|✔|Site|SP.Site object|…/_api/site|
|❌|TimeZone|SP.TimeZone object|…/_api/web/RegionalSettings/TimeZones(timzoneid)|
|❌|TimeZoneCollection|SP.TimeZoneCollection object|…/_api/web/RegionalSettings/TimeZones|
|❌|User|SP.User object|…/_api/web/siteusers(@v)?@v=''|
|❌|UserCollection|SP.UserCollection object|…/_api/web/sitegroups()/users|
|❌|View|SP.View object (sp.js)|…/_api/web/lists(guid'')/views('')|
|❌|ViewCollection|SP.ViewCollection object|…/_api/web/lists(guid'')/views|
|❌|ViewFieldCollection|SP.ViewFieldCollection object|…/_api/web/lists(guid'')/views('')/fields|
|✔|Web|SP.Web object|…/_api/web|
|✔|WebCollection|SP.WebCollection object|…/_api/web/webs|
|✔|WebInformation|SP.WebInformation object|…/_api/web/webinfos('<web information id>')|
|❌|WebTemplate|SP.WebTemplate object|…/_api/web/GetAvailableWebTemplates(languageid,includecrosslanguage)/getbyname(templatename)|
|❌|WebTemplateCollection|SP.WebTemplateCollection object|…/_api/web/GetAvailableWebTemplates(languageid,includecrosslanguage)|
|| | | .|
|❔|Site Design / Site Script||[Site Design REST API](https://docs.microsoft.com/en-us/sharepoint/dev/declarative-customization/site-design-rest-api)|
|❔|Site theming||[Site Theming REST API](https://docs.microsoft.com/en-us/sharepoint/dev/declarative-customization/site-theming/sharepoint-site-theming-rest-api)|
|❔|SharePoint Search||[SharePoint Search REST API](https://docs.microsoft.com/en-us/sharepoint/dev/general-development/sharepoint-search-rest-api-overview)|
|❔|SharePoint User Profile||[SharePoint User Profile REST reference](https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-rest-reference/dn790354(v=office.15))|
|❔|SharePoint List webhooks||[SharePoint List Webhooks](https://docs.microsoft.com/en-us/sharepoint/dev/apis/webhooks/lists/overview-sharepoint-list-webhooks)|
|❔|Hub Sites||[Hub Sites REST API](https://docs.microsoft.com/en-us/sharepoint/dev/features/hub-site/hub-site-rest-api)|
|❔|SPFx ALM||[ALM API for SPFx Add-Ins](https://docs.microsoft.com/en-us/sharepoint/dev/apis/alm-api-for-spfx-add-ins)|
