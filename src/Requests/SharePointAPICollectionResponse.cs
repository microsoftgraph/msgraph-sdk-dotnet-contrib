using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SharePointAPICollectionResponse<T>
  {
    /// <summary>
    /// Gets or sets the CollectionPage value.
    /// </summary>
    [JsonPropertyName("value")]
    public T Value { get; set; }

    /// <summary>
    /// Gets or sets additional data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object> AdditionalData { get; set; }

  }
}
