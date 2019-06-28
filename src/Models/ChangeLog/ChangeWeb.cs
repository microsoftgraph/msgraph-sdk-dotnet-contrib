using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ChangeWeb :Change
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "WebId", Required = Newtonsoft.Json.Required.Default)]
		public Guid WebId { get; set; }
	}
}
