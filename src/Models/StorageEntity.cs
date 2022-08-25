using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class StorageEntity
  {
    [JsonPropertyName("Comment")]
    public string Comment { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("Value")]
    public string Value { get; set; }
  }
}
