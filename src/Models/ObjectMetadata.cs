using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ObjectMetadata
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Newtonsoft.Json.Required.Default)]
		public string Type { get; set; }
	}
}
