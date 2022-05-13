using System;
using System.Text.Json.Serialization;
using Microsoft.Graph;

namespace Graph.Community
{
  [JsonConverter(typeof(SPNavigationNodeConverter))]
  public class NavigationNode : BaseItem
  {
    [JsonPropertyName("Id")]
    public new int Id { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Url")]
    public Uri Url { get; set; }

    [JsonPropertyName("IsDocLib")]
    public bool IsDocLib { get; set; }

    [JsonPropertyName("IsExternal")]
    public bool IsExternal { get; set; }

    [JsonPropertyName("IsVisible")]
    public bool IsVisible { get; set; }

    [JsonPropertyName("ListTemplateType")]
    public int ListTemplateType { get; set; }
  }
}
