using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptOut)]
	[JsonConverter(typeof(SPDerivedTypedConverter))]
	public class Group: Principal
  {
		[JsonProperty(PropertyName = "AllowMembersEditMembership")]
		public bool AllowMembersEditMembership { get; set; }

		[JsonProperty(PropertyName = "AllowRequestToJoinLeave")]
		public bool AllowRequestToJoinLeave { get; set; }

		[JsonProperty(PropertyName = "AutoAcceptRequestToJoinLeave")]
		public bool AutoAcceptRequestToJoinLeave { get; set; }

		[JsonProperty]
		public new string Description { get; set; }

    [JsonProperty(PropertyName = "OnlyAllowMembersViewMembership")]
		public bool OnlyAllowMembersViewMembership { get; set; }

		[JsonProperty(PropertyName = "Owner@odata.navigationLinkUrl")]
    public string OwnerNavigationLink { get; set; }

    [JsonProperty]
    public Principal Owner { get; set; }

    [JsonProperty]
    public string OwnerTitle { get; set; }

    [JsonProperty(PropertyName = "RequestToJoinLeaveEmailSetting")]
		public string RequestToJoinLeaveEmailSetting { get; set; }

		[JsonProperty(PropertyName = "Users@odata.navigationLink")]
    public string UsersNavigationLink { get; set; }

    [JsonProperty]
		public List<User> Users { get; }

		public Group()
    {
			this.Users = new List<User>();
    }
	}
}
