using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Web:BaseItem
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "CurrentChangeToken", Required = Newtonsoft.Json.Required.Default)]
		public ChangeToken CurrentChangeToken { get; set; }

		//[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Id", Required = Newtonsoft.Json.Required.Default)]
		//public string Id { get; set; }
	}
}
