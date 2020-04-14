using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class SiteScriptMetadata : BaseItem
  {
    /// <summary>
    /// The display name of the site script.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Title", Required = Newtonsoft.Json.Required.Default)]
    public string Title { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Version", Required = Newtonsoft.Json.Required.Default)]
    public int Version { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Content", Required = Newtonsoft.Json.Required.Default)]
    public string Content { get; set; }
  }
}
