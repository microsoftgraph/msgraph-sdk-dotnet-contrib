namespace Graph.Community
{
  /// <summary>
  /// Indicates if a file was published, draft or checked out
  /// </summary>
  public enum SitePagePublishedStatus
  {
    /// <summary>
    /// File is published
    /// </summary>
    Published = 1,

    /// <summary>
    /// File is in draft
    /// </summary>
    Draft = 2,

    /// <summary>
    /// File is checked out
    /// </summary>
    Checkout = byte.MaxValue
  }
}
