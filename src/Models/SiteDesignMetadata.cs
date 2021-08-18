using Microsoft.Graph;
using System;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SiteDesignMetadata : BaseItem
  {
    public Guid? DesignPackageId { get; set; }

    /// <summary>
    /// (Optional) True if the site design is applied as the default site design; otherwise, false. For more information see Customize a default site design.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// (Optional) The alt text description of the image for accessibility.
    /// </summary>
    public string PreviewImageAltText { get; set; }

    /// <summary>
    /// (Optional) The URL of a preview image. If none is specified, SharePoint uses a generic image.
    /// </summary>
    public string PreviewImageUrl { get; set; }

    /// <summary>
    /// An array of one or more site scripts. Each is identified by an ID. The scripts will run in the order listed.
    /// </summary>
    public List<Guid> SiteScriptIds { get; set; }

    /// <summary>
    /// The display name of the site design.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Identifies which base template to add the design to. Use the value 64 for the Team site template, and the value 68 for the Communication site template.
    /// </summary>
    public string WebTemplate { get; set; }

    public int Version { get; set; }
  }
}
