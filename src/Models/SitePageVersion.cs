using System;
using System.Text.Json.Serialization;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageVersion : BaseItem
  {
    public new int Id { get; set; }

    //public string CheckInComment { get; set; }
    public DateTime Created { get; set; }
    public bool IsCurrentVersion { get; set; }

    // Refer to https://github.com/pnp/pnpcore/issues/581
    //public int Length { get; set; }

    public long Size { get; set; }

    public string VersionLabel { get; set; }
  }
}
