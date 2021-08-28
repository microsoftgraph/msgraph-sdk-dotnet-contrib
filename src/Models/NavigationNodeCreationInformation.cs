using System;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class NavigationNodeCreationInformation
  {
    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Url")]
    public Uri Url { get; set; }
  }
}
