using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class SiteDesignRun: BaseItem
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SiteDesignId", Required = Newtonsoft.Json.Required.Default)]
    public Guid SiteDesignId { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SiteDesignTitle", Required = Newtonsoft.Json.Required.Default)]
    public string SiteDesignTitle { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SiteDesignVersion", Required = Newtonsoft.Json.Required.Default)]
    public int SiteDesignVersion { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SiteId", Required = Newtonsoft.Json.Required.Default)]
    public Guid SiteId { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "WebId", Required = Newtonsoft.Json.Required.Default)]
    public Guid WebId { get; set; }

    /// <summary>
    /// StartTime - Appears to be the Unix Epoch timestamp
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "StartTime", Required = Newtonsoft.Json.Required.Default)]
    public Int64 StartTime { get; set; }
  }
}
