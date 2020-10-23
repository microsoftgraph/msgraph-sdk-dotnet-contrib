using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
#pragma warning disable CA1724 //Type names should not match namespaces

  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  [JsonConverter(typeof(SPDerivedTypedConverter))]
  public class Web : BaseItem
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "CurrentChangeToken", Required = Newtonsoft.Json.Required.Default)]
    public ChangeToken CurrentChangeToken { get; set; }

    [JsonProperty(PropertyName = "Users@odata.navigationLink")]
    public string UsersNavigationLink { get; set; }

    [JsonProperty]
    public List<User> Users { get; }

    [JsonProperty(PropertyName = "AssociatedMemberGroup@odata.navigationLink")]
    public string AssociatedMemberGroupNavigationLink { get; set; }

    [JsonProperty]
    public Group AssociatedMemberGroup { get; set; }

    [JsonProperty(PropertyName = "AssociatedOwnerGroup@odata.navigationLink")]
    public string AssociatedOwnerGroupNavigationLink { get; set; }

    [JsonProperty]
    public Group AssociatedOwnerGroup { get; set; }

    [JsonProperty(PropertyName = "AssociatedVisitorGroup@odata.navigationLink")]
    public string AssociatedVisitorGroupNavigationLink { get; set; }

    [JsonProperty]
    public Group AssociatedVisitorGroup { get; set; }
  }
#pragma warning restore CA1724 //Type names should not match namespaces
}
