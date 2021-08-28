using Microsoft.Graph;

namespace Graph.Community
{
  public class Site : BaseItem
  {
    public string Classification { get; set; }

    public string GroupId { get; set; }

    public string HubSiteId { get; set; }

    public bool IsHubSite { get; set; }

    public string ServerRelativeUrl { get; set; }
  }
}
