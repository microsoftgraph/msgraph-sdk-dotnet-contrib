using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class ChangeLogCollectionResponse
  {
    /// <summary>
    /// Gets or sets the <see cref="IChangeLogCollectionPage"/> value.
    /// </summary>
    [JsonPropertyName("value")]
    public IChangeLogCollectionPage Value { get; set; }

    /// <summary>
    /// Gets or sets additional data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object> AdditionalData { get; set; }

  }
}
