using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class GetChangesRequest
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "query", Required = Newtonsoft.Json.Required.Default)]
    public ChangeQuery Query { get; set; }
  }
}
