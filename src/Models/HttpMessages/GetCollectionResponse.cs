using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class GetCollectionResponse<T> where T : BaseItem
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value", Required = Newtonsoft.Json.Required.Default)]
    public CollectionPage<T> Value { get; }

    public GetCollectionResponse()
    {
      this.Value = new CollectionPage<T>();
    }
  }
}
