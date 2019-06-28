using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ChangeItem : Change
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ItemId", Required = Newtonsoft.Json.Required.Default)]
		public int ItemId { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ListId", Required = Newtonsoft.Json.Required.Default)]
		public Guid ListId { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "UniqueId", Required = Newtonsoft.Json.Required.Default)]
		public Guid UniqueId { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "WebId", Required = Newtonsoft.Json.Required.Default)]
		public Guid WebId { get; set; }
	}
}
