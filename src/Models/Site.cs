using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Site : BaseItem
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Classification", Required = Newtonsoft.Json.Required.Default)]
		public string Classification { get; set; }

		// "CurrentChangeToken": {
		//"__metadata": {
		//	"type": "SP.ChangeToken"
		//   },
		//   "StringValue": "1;1;a0ea50ed-6c77-4d79-9f03-8c6acfbf18b6;636970246501570000;229976113"
		// },

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "CurrentChangeToken", Required = Newtonsoft.Json.Required.Default)]
		public ChangeToken CurrentChangeToken { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "GroupId", Required = Newtonsoft.Json.Required.Default)]
		public string  GroupId { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "HubSiteId", Required = Newtonsoft.Json.Required.Default)]
		public string HubSiteId { get; set; }

		//[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Id", Required = Newtonsoft.Json.Required.Default)]
		//public string Id { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "IsHubSite", Required = Newtonsoft.Json.Required.Default)]
		public bool IsHubSite { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ServerRelativeUrl", Required = Newtonsoft.Json.Required.Default)]
		public string ServerRelativeUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings
	}
}
