namespace Graph.Community
{
  public class Hub
  {
    /// <summary>
    /// Identifies the hub site.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The display name of the hub site.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// ID of the hub parent site.
    /// </summary>
    public string SiteId { get; set; }

    /// <summary>
    /// The tenant instance ID in which the hub site is located. Use empty GUID for the default tenant instance.
    /// </summary>
    public string TenantInstanceId { get; set; }

    /// <summary>
    /// URL of the hub parent site.
    /// </summary>
    public string SiteUrl { get; set; }

    /// <summary>
    /// The URL of a logo to use in the hub site navigation.
    /// </summary>
    public string LogoUrl { get; set; }

    /// <summary>
    /// A description of the hub site.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// List of security groups with access to join the hub site. Null if everyone has permission.
    /// </summary>
    public string Targets { get; set; }

    public bool RequiresJoinApproval { get; set; }
  }
}
