using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(SPChangeDerivedTypedConverter))]
	public class Change : BaseItem, IChange
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ChangeToken", Required = Newtonsoft.Json.Required.Default)]
		public ChangeToken ChangeToken { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ChangeType", Required = Newtonsoft.Json.Required.Default)]
		public int ChangeType { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SiteId", Required = Newtonsoft.Json.Required.Default)]
		public Guid SiteId { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Time", Required = Newtonsoft.Json.Required.Default)]
		public DateTime Time { get; set; }


	}
}
