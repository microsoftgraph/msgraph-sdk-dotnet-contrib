using System.Text.Json.Serialization;

namespace Graph.Community
{
  [JsonConverter(typeof(SPChangeQueryConverter))]
  public class ChangeQuery
  {
    public bool Activity { get; set; }

    public bool Add { get; set; }

    public bool Alert { get; set; }

    public ChangeToken ChangeTokenEnd { get; set; }

    public ChangeToken ChangeTokenStart { get; set; }

    public bool ContentType { get; set; }

    public bool DeleteObject { get; set; }

    public int FetchLimit { get; set; }

    public bool Field { get; set; }

    public bool File { get; set; }

    public bool Folder { get; set; }

    public bool Group { get; set; }

    public bool GroupMembershipAdd { get; set; }

    public bool GroupMembershipDelete { get; set; }

    public bool Item { get; set; }

    public bool LatestFirst { get; set; }

    public bool List { get; set; }

    public bool Move { get; set; }

    public bool Navigation { get; set; }

    public bool RecursiveAll { get; set; }

    public bool Rename { get; set; }

    public bool RequireSecurityTrim { get; set; }

    public bool Restore { get; set; }

    public bool RoleAssignmentAdd { get; set; }

    public bool RoleAssignmentDelete { get; set; }

    public bool RoleDefinitionAdd { get; set; }

    public bool RoleDefinitionDelete { get; set; }

    public bool RoleDefinitionUpdate { get; set; }

    public bool SecurityPolicy { get; set; }

    public bool Site { get; set; }

    public bool SystemUpdate { get; set; }

    public bool Update { get; set; }

    public bool User { get; set; }

    public bool View { get; set; }

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
