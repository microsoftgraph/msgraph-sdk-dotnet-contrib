using Microsoft.Graph;

namespace Graph.Community
{
  public class SiteScriptMetadata : BaseItem
  {
    /// <summary>
    /// The display name of the site script.
    /// </summary>
    public string Title { get; set; }

    public int Version { get; set; }

    public string Content { get; set; }
  }
}
