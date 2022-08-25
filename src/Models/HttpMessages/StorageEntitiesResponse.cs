using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class StorageEntitiesResponse
  {
    [JsonPropertyName("storageentitiesindex")]
    public string StorageEntitiesIndex { get; set; }
  }
}
