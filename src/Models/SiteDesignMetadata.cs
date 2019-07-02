using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
#pragma warning disable CA1056 // Uri properties should not be strings
#pragma warning disable CA2227 // Collection properties should be read only

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SiteDesignMetadata : BaseItem
	{
		//[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Id", Required = Newtonsoft.Json.Required.Default)]
		//public Guid Id { get; set; }

		//[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Description", Required = Newtonsoft.Json.Required.Default)]
		//public string Description { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "DesignPackageId", Required = Newtonsoft.Json.Required.Default)]
		public Guid DesignPackageId { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "IsDefault", Required = Newtonsoft.Json.Required.Default)]
		public bool IsDefault { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "PreviewImageAltText", Required = Newtonsoft.Json.Required.Default)]
		public string PreviewImageAltText { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "PreviewImageUrl", Required = Newtonsoft.Json.Required.Default)]
		public string PreviewImageUrl { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SiteScriptIds", Required = Newtonsoft.Json.Required.Default)]
		public List<Guid> SiteScriptIds { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Title", Required = Newtonsoft.Json.Required.Default)]
		public string Title { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "WebTemplate", Required = Newtonsoft.Json.Required.Default)]
		public string WebTemplate { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Version", Required = Newtonsoft.Json.Required.Default)]
		public int Version { get; set; }
	}

#pragma warning restore CA1056 // Uri properties should not be strings
#pragma warning restore CA2227 // Collection properties should be read only
}
