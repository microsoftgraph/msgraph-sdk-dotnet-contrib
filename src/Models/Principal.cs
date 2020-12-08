using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  [JsonConverter(typeof(SPDerivedTypedConverter))]
  public class Principal: BaseItem
  {
    /// <summary>
    /// Gets a value that specifies the member identifier for the user or group.
    /// </summary>
    [JsonProperty(PropertyName = "Id")]
    public new int Id { get; set; }

    /// <summary>
    /// Gets a value that indicates whether this member should be hidden in the UI.
    /// </summary>
    [JsonProperty(PropertyName = "IsHiddenInUI")]
    public bool IsHiddenInUI { get; set; }

    /// <summary>
    /// Gets a value containing the type of the principal. Represents a bitwise SP.PrincipalType value: None = 0; User = 1; DistributionList = 2; SecurityGroup = 4; SharePointGroup = 8; All = 15.
    /// </summary>
    [JsonProperty(PropertyName = "PrincipalType")]
    public SPPrincipalType PrincipalType { get; set; }

    /// <summary>
    /// Gets the login name of the user.
    /// </summary>
    [JsonProperty(PropertyName = "LoginName")]
    public string LoginName { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies the name of the principal.
    /// </summary>
    [JsonProperty(PropertyName = "Title")]
    public string Title { get; set; }
  }
}
