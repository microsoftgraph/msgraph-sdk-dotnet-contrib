using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GetSiteDesignCollectionResponse
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value", Required = Newtonsoft.Json.Required.Default)]
		public CollectionPage<SiteDesignMetadata> Value { get; set; }
	}
}
