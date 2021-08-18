using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community.Samples
{
  public class SharePointSettings
  {
    public const string ConfigurationSectionName = "SharePoint";

    public string Hostname { get; set; }
    public string SiteCollectionUrl { get; set; }
  }
}
