using System.Collections.Generic;

namespace Graph.Community
{
  public static class GraphGroupExtensions
  {
    public static void AddMember(this Microsoft.Graph.Group group, string userId)
    {
      if (group.AdditionalData == null)
      {
        group.AdditionalData = new Dictionary<string, object>();
      }

      string[] membersToAdd = new string[1];
      membersToAdd[0] = $"https://graph.microsoft.com/v1.0/users/{userId}";
      group.AdditionalData.Add("members@odata.bind", membersToAdd);
    }

    public static void AddOwner(this Microsoft.Graph.Group group, string userId)
    {
      if (group.AdditionalData == null)
      {
        group.AdditionalData = new Dictionary<string, object>();
      }

      string[] ownersToAdd = new string[1];
      ownersToAdd[0] = $"https://graph.microsoft.com/v1.0/users/{userId}";
      group.AdditionalData.Add("owners@odata.bind", ownersToAdd);
    }

  }
}
