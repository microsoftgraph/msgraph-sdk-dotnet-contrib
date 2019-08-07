using Microsoft.Graph.Auth;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Graph.Community.Samples
{
	public class AzureAdOptions
	{
		public string TenantId { get; set; }
		public string ClientId { get; set; }
	}
}
