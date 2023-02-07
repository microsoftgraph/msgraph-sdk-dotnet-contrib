using System.Collections.Generic;
using System.Text.Json.Serialization;
using Graph.Community.Models;
using Microsoft.Graph;

namespace Graph.Community
{
  [SPDerivedTypeConverter(typeof(SPODataTypeConverter<Web>))]
  public class Web : BaseItem
  {
    [JsonPropertyName("Id")]
    public new string Id { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("CurrentChangeToken")]
    public ChangeToken CurrentChangeToken { get; set; }

    [JsonPropertyName("UsersNavigationLink")]
    public string UsersNavigationLink { get; set; }

    [JsonPropertyName("Users")]
    public List<User> Users { get; }

    [JsonPropertyName("AssociatedMemberGroupNavigationLink")]
    public string AssociatedMemberGroupNavigationLink { get; set; }

    [JsonPropertyName("AssociatedMemberGroup")]
    public Group AssociatedMemberGroup { get; set; }

    [JsonPropertyName("AssociatedOwnerGroupNavigationLink")]
    public string AssociatedOwnerGroupNavigationLink { get; set; }

    [JsonPropertyName("AssociatedOwnerGroup")]
    public Group AssociatedOwnerGroup { get; set; }

    [JsonPropertyName("AssociatedVisitorGroupNavigationLink")]
    public string AssociatedVisitorGroupNavigationLink { get; set; }

    [JsonPropertyName("AssociatedVisitorGroup")]
    public Group AssociatedVisitorGroup { get; set; }

    [JsonPropertyName("WelcomePage")]
    public string WelcomePage { get; set; }

    [JsonPropertyName("RegionalSettings")]
    public RegionalSettings RegionalSettings { get; set; }
  }
}
