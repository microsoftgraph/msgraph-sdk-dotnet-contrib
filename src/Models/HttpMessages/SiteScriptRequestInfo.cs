using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteScriptRequestInfo
  {
    [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Title { get; set; }

    [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Description { get; set; }

    [JsonProperty("Version", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int Version { get; set; }

    [JsonProperty("Content", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Content { get; set; }
  }
}
