using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class ChangeQuery
  {
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Activity", Required = Newtonsoft.Json.Required.Default)]
    public bool Activity { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Add", Required = Newtonsoft.Json.Required.Default)]
    public bool Add { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Alert", Required = Newtonsoft.Json.Required.Default)]
    public bool Alert { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ChangeTokenEnd", Required = Newtonsoft.Json.Required.Default)]
    public ChangeToken ChangeTokenEnd { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ChangeTokenStart", Required = Newtonsoft.Json.Required.Default)]
    public ChangeToken ChangeTokenStart { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "ContentType", Required = Newtonsoft.Json.Required.Default)]
    public bool ContentType { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "DeleteObject", Required = Newtonsoft.Json.Required.Default)]
    public bool DeleteObject { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "FetchLimit", Required = Newtonsoft.Json.Required.Default)]
    public int FetchLimit { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Field", Required = Newtonsoft.Json.Required.Default)]
    public bool Field { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "File", Required = Newtonsoft.Json.Required.Default)]
    public bool File { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Folder", Required = Newtonsoft.Json.Required.Default)]
    public bool Folder { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Group", Required = Newtonsoft.Json.Required.Default)]
    public bool Group { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "GroupMembershipAdd", Required = Newtonsoft.Json.Required.Default)]
    public bool GroupMembershipAdd { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "GroupMembershipDelete", Required = Newtonsoft.Json.Required.Default)]
    public bool GroupMembershipDelete { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Item", Required = Newtonsoft.Json.Required.Default)]
    public bool Item { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "LatestFirst", Required = Newtonsoft.Json.Required.Default)]
    public bool LatestFirst { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "List", Required = Newtonsoft.Json.Required.Default)]
    public bool List { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Move", Required = Newtonsoft.Json.Required.Default)]
    public bool Move { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Navigation", Required = Newtonsoft.Json.Required.Default)]
    public bool Navigation { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "RecursiveAll", Required = Newtonsoft.Json.Required.Default)]
    public bool RecursiveAll { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "RequireSecurityTrim", Required = Newtonsoft.Json.Required.Default)]
    public bool Rename { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Rename", Required = Newtonsoft.Json.Required.Default)]
    public bool RequireSecurityTrim { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Restore", Required = Newtonsoft.Json.Required.Default)]
    public bool Restore { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "RoleAssignmentAdd", Required = Newtonsoft.Json.Required.Default)]
    public bool RoleAssignmentAdd { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "RoleAssignmentDelete", Required = Newtonsoft.Json.Required.Default)]
    public bool RoleAssignmentDelete { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "RoleDefinitionAdd", Required = Newtonsoft.Json.Required.Default)]
    public bool RoleDefinitionAdd { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "RoleDefinitionDelete", Required = Newtonsoft.Json.Required.Default)]
    public bool RoleDefinitionDelete { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "RoleDefinitionUpdate", Required = Newtonsoft.Json.Required.Default)]
    public bool RoleDefinitionUpdate { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "SecurityPolicy", Required = Newtonsoft.Json.Required.Default)]
    public bool SecurityPolicy { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Site", Required = Newtonsoft.Json.Required.Default)]
    public bool Site { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "SystemUpdate", Required = Newtonsoft.Json.Required.Default)]
    public bool SystemUpdate { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Update", Required = Newtonsoft.Json.Required.Default)]
    public bool Update { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "User", Required = Newtonsoft.Json.Required.Default)]
    public bool User { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "View", Required = Newtonsoft.Json.Required.Default)]
    public bool View { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "Web", Required = Newtonsoft.Json.Required.Default)]
    public bool Web { get; set; }

    public ChangeQuery()
      : this(false, false)
    {
    }

    public ChangeQuery(bool allObjectTypes, bool allChangeTypes)
    {
      if (allObjectTypes)
      {
        this.Alert = true;
        this.ContentType = true;
        this.Field = true;
        this.File = true;
        this.Folder = true;
        this.Group = true;
        this.Item = true;
        this.List = true;
        this.SecurityPolicy = true;
        this.Site = true;
        this.User = true;
        this.View = true;
        this.Web = true;
      }

      if (allChangeTypes)
      {
        this.Add = true;
        this.DeleteObject = true;
        this.GroupMembershipAdd = true;
        this.GroupMembershipDelete = true;
        this.Move = true;
        this.Navigation = true;
        this.Rename = true;
        this.Restore = true;
        this.RoleAssignmentAdd = true;
        this.RoleAssignmentDelete = true;
        this.RoleDefinitionAdd = true;
        this.RoleDefinitionDelete = true;
        this.RoleDefinitionUpdate = true;
        this.SystemUpdate = true;
        this.Update = true;
      }
    }
  }

}
