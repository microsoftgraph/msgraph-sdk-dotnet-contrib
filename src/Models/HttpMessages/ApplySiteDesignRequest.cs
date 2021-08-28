namespace Graph.Community
{
  public class ApplySiteDesignRequest
  {
    /// <summary>
    /// Id of the site design to apply
    /// </summary>
    public string SiteDesignId { get; set; }

    /// <summary>
    /// Absolute URL of site (site collection root) to which design is applied
    /// </summary>
    public string WebUrl { get; set; }
  }
}
