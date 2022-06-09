using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public enum SitePageModerationStatus
  {
    /// <summary>
    /// The list item is approved.
    /// </summary>
    Approved,
    /// <summary>
    /// The list item has been denied approval.
    /// </summary>
    Denied,
    /// <summary>
    /// The list item is pending approval.
    /// </summary>
    Pending,
    /// <summary>
    /// The list item is in the draft or checked out state.
    /// </summary>
    Draft,
    /// <summary>
    /// The list item is scheduled for automatic approval at a future date.
    /// </summary>
    Scheduled
  }
}
