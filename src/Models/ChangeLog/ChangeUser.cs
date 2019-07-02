using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ChangeUser : Change
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Activate", Required = Newtonsoft.Json.Required.Default)]
		public bool Activate { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "UserId", Required = Newtonsoft.Json.Required.Default)]
		public int UserId { get; set; }

	}
}
