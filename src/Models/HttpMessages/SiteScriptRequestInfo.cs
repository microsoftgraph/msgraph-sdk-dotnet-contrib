using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SiteScriptRequestInfo
  {
    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("Version")]
    public int? Version { get; set; }

    [JsonPropertyName("Content")]
    public string Content { get; set; }
  }
}
