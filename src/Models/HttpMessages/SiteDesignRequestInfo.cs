using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteDesignRequestInfo
  {
    [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Title { get; set; }

    [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Description { get; set; }

    [JsonProperty("SiteScriptIds", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public List<Guid> SiteScriptIds { get; set; }

    [JsonProperty("WebTemplate", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string WebTemplate { get; set; }

    [JsonProperty("PreviewImageUrl", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string PreviewImageUrl { get; set; }

    [JsonProperty("PreviewImageAltText", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string PreviewImageAltText { get; set; }

    [JsonProperty("ThumbnailUrl", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string ThumbnailUrl { get; set; }

  }
}
