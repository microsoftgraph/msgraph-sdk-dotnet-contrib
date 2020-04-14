using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
  public class NavigationNode : BaseItem
  {
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public new int Id { get; set; }

    public string Title { get; set; }

    public Uri Url { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsDocLib { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsExternal { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsVisible { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int ListTemplateType { get; set; }
  }
}
