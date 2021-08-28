using System.Text.Json.Serialization;

namespace Graph.Community
{
  [SPDerivedTypeConverter(typeof(SPODataTypeConverter<User>))]
  public class User : Principal
  {
    [JsonPropertyName("Email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets a Boolean value that specifies whether the user is a site collection administrator.
    /// </summary>
    [JsonPropertyName("IsSiteAdmin")]
    public bool IsSiteAdmin { get; set; }

    [JsonPropertyName("IsEmailAuthenticationGuestUser")]
    public bool IsEmailAuthenticationGuestUser { get; set; }

    [JsonPropertyName("IsShareByEmailGuestUser")]
    public bool IsShareByEmailGuestUser { get; set; }

    [JsonPropertyName("UserPrincipalName")]
    public string UserPrincipalName { get; set; }

    [JsonPropertyName("UserId")]
    public UserId UserId { get; set; }
  }

  public class UserId
  {
    [JsonPropertyName("NameId")]
    public string NameId { get; set; }
    [JsonPropertyName("NameIdIssuer")]
    public string NameIdIssuer { get; set; }
  }
}
