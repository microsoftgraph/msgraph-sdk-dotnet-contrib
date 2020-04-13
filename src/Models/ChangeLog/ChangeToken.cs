using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class ChangeToken
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "StringValue", Required = Newtonsoft.Json.Required.Default)]
    public string StringValue { get; set; }
  }
}
