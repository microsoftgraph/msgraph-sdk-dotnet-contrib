using Microsoft.Graph;

namespace Graph.Community
{
  public class Site : BaseItem
  {
    public string Classification { get; set; }

    public string GroupId { get; set; }

    public string HubSiteId { get; set; }

    public bool IsHubSite { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
    public string ServerRelativeUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings
  }
}
