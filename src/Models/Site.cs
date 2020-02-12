using Microsoft.Graph;
using Newtonsoft.Json;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class Site : BaseItem
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Classification", Required = Newtonsoft.Json.Required.Default)]
    public string Classification { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "GroupId", Required = Newtonsoft.Json.Required.Default)]
    public string GroupId { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "HubSiteId", Required = Newtonsoft.Json.Required.Default)]
    public string HubSiteId { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "IsHubSite", Required = Newtonsoft.Json.Required.Default)]
    public bool IsHubSite { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ServerRelativeUrl", Required = Newtonsoft.Json.Required.Default)]
    public string ServerRelativeUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings
  }
}
