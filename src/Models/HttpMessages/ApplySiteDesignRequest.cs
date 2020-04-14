using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class ApplySiteDesignRequest
  {
    /// <summary>
    /// Id of the site design to apply
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "siteDesignId", Required = Newtonsoft.Json.Required.Always)]
    public string SiteDesignId { get; set; }

    /// <summary>
    /// Absolute URL of site (site collection root) to which design is applied
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "webUrl", Required = Newtonsoft.Json.Required.Always)]
    public string WebUrl { get; set; }
  }
}
