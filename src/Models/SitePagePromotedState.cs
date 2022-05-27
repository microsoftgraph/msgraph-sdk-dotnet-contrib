using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public enum SitePagePromotedState
  {
    /// <summary>
    /// Regular page
    /// </summary>
    NotPromoted = 0,

    /// <summary>
    /// Page that will be promoted as news article after publishing
    /// </summary>
    PromoteOnPublish = 1,

    /// <summary>
    /// Page that is promoted as news article
    /// </summary>
    Promoted = 2
  }
}
