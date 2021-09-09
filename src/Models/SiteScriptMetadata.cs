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
    [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }

    [JsonProperty("Version", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int Version { get; set; }

    [JsonProperty("Content", NullValueHandling = NullValueHandling.Ignore)]
    public string Content { get; set; }

    [JsonProperty("IsSiteScriptPackage", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsSiteScriptPackage { get; set; }
  }
}
