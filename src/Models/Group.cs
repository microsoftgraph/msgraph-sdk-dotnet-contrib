using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class Group: Principal
  {
		public bool AllowMembersEditMembership { get; set; }

		public bool AllowRequestToJoinLeave { get; set; }

		public bool AutoAcceptRequestToJoinLeave { get; set; }

		public new string Description { get; set; }

		public bool OnlyAllowMembersViewMembership { get; set; }

    public string OwnerNavigationLink { get; set; }

    public Principal Owner { get; set; }

    public string OwnerTitle { get; set; }

		public string RequestToJoinLeaveEmailSetting { get; set; }

    public string UsersNavigationLink { get; set; }

		public List<User> Users { get; }

		public Group()
    {
			this.Users = new List<User>();
    }
	}
}
