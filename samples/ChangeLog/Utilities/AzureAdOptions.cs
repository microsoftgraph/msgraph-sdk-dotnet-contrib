using Microsoft.Graph.Auth;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Graph.Community.Samples.CommandLine
{
	public class AzureAdOptions
	{
		public string TenantId { get; set; }
		public string ClientId { get; set; }
		public string SharePointDomain { get; set; }
		public string Username { get; set; }

		public string Password
		{
			get { return null; }
			set
			{
				this.SecurePassword = new SecureString();
				foreach (char c in value) { this.SecurePassword.AppendChar(c); }
			}
		}
		public SecureString SecurePassword { get; private set; }

		public string[] Scopes { get; set; }
	}
}
