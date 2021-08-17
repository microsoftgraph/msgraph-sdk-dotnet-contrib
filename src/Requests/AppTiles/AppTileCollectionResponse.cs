using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class AppTileCollectionResponse
  {
    /// <summary>
    /// Gets or sets the <see cref="IAppTileCollectionPage"/> value.
    /// </summary>
    [JsonPropertyName("value")]
    public IAppTileCollectionPage Value { get; set; }


    /// <summary>
    /// Gets or sets additional data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object> AdditionalData { get; set; }
  }
}
