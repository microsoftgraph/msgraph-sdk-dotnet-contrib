using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  [Obsolete("Use XXXCollectionPage classes for v4", false)]
  public class GetCollectionResponse<T> where T : BaseItem
  {
    [JsonPropertyName("value")]
    public CollectionPage<T> Value { get; }

    public GetCollectionResponse()
    {
      this.Value = new CollectionPage<T>();
    }
  }
}
