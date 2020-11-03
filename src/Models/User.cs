using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  [JsonConverter(typeof(SPDerivedTypedConverter))]
  public class User : Principal
  {

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [JsonProperty(PropertyName = "Email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets a Boolean value that specifies whether the user is a site collection administrator.
    /// </summary>
    [JsonProperty(PropertyName = "IsSiteAdmin")]
    public bool IsSiteAdmin { get; set; }

    [JsonProperty(PropertyName = "UserPrincipalName")]
    public string UserPrincipalName { get; set; }

    [JsonProperty(PropertyName ="UserId")]
    public UserId UserId { get; set; }
  }

  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class UserId
  {
    [JsonProperty(PropertyName = "NameId")]
    public string NameId { get; set; }
    [JsonProperty(PropertyName = "NameIdIssuer")]
    public string NameIdIssuer { get; set; }
  }
}
