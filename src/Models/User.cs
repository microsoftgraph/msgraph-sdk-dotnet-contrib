using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class User : Principal
	{

		/// <summary>
		/// Gets or sets the email address of the user.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets a Boolean value that specifies whether the user is a site collection administrator.
		/// </summary>
		public bool IsSiteAdmin { get; set; }

		public bool IsEmailAuthenticationGuestUser { get; set; }

		public bool IsShareByEmailGuestUser { get; set; }

		public string UserPrincipalName { get; set; }

		public UserId UserId { get; set; }
	}

	public class UserId
	{
		public string NameId { get; set; }
		public string NameIdIssuer { get; set; }
	}
}
