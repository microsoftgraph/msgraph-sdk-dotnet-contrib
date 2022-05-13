using System;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SiteDesignRun : BaseItem
  {
    public Guid SiteDesignId { get; set; }

    public string SiteDesignTitle { get; set; }

    public int SiteDesignVersion { get; set; }

    public Guid SiteId { get; set; }

    public Guid WebId { get; set; }

    /// <summary>
    /// StartTime - Appears to be the Unix Epoch timestamp
    /// </summary>
    public Int64 StartTime { get; set; }
  }
}
