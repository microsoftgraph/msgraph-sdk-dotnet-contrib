using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
#pragma warning disable CA1724 //Type names should not match namespaces

  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class Web : BaseItem
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "CurrentChangeToken", Required = Newtonsoft.Json.Required.Default)]
    public ChangeToken CurrentChangeToken { get; set; }
  }
#pragma warning restore CA1724 //Type names should not match namespaces
}
