using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  [SPDerivedTypeConverter(typeof(SPODataTypeConverter<Group>))]
  public class Group : Principal
  {
    [JsonPropertyName("AllowMembersEditMembership")]
    public bool AllowMembersEditMembership { get; set; }

    [JsonPropertyName("AllowRequestToJoinLeave")]
    public bool AllowRequestToJoinLeave { get; set; }

    [JsonPropertyName("AutoAcceptRequestToJoinLeave")]
    public bool AutoAcceptRequestToJoinLeave { get; set; }

    [JsonPropertyName("Description")]
    public new string Description { get; set; }

    [JsonPropertyName("OnlyAllowMembersViewMembership")]
    public bool OnlyAllowMembersViewMembership { get; set; }

    [JsonPropertyName("Owner@odata.navigationLink")]
    public string OwnerNavigationLink { get; set; }

    [JsonPropertyName("Owner")]
    public Principal Owner { get; set; }

    [JsonPropertyName("OwnerTitle")]
    public string OwnerTitle { get; set; }

    [JsonPropertyName("RequestToJoinLeaveEmailSetting")]
    public string RequestToJoinLeaveEmailSetting { get; set; }

    [JsonPropertyName("Users@odata.navigationLink")]
    public string UsersNavigationLink { get; set; }

    [JsonPropertyName("Users")]
    public List<User> Users { get; private set; }

    public Group()
    {
      this.Users = new List<User>();
    }
  }
}
