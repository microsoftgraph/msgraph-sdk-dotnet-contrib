using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
#pragma warning disable CA1056 // Uri properties should not be strings
#pragma warning disable CA2227 // Collection properties should be read only

  public class SiteDesignMetadata : BaseItem
  {
    [JsonProperty(PropertyName = "DesignPackageId", NullValueHandling = NullValueHandling.Ignore)]
    public Guid DesignPackageId { get; set; }

    public string DesignType { get; set; }

    /// <summary>
    /// (Optional) True if the site design is applied as the default site design; otherwise, false. For more information see Customize a default site design.
    /// </summary>
    [JsonProperty(PropertyName = "IsDefault", NullValueHandling = NullValueHandling.Ignore)]
    public bool IsDefault { get; set; }

    public bool IsOutOfBoxTemplate { get; set; }
    public bool IsTenantAdminOnly { get; set; }
    public string ListColor { get; set; }
    public string ListIcon { get; set; }


    /// <summary>
    /// (Optional) The alt text description of the image for accessibility.
    /// </summary>
    [JsonProperty(PropertyName = "PreviewImageAltText", NullValueHandling = NullValueHandling.Ignore)]
    public string PreviewImageAltText { get; set; }

    /// <summary>
    /// (Optional) The URL of a preview image. If none is specified, SharePoint uses a generic image.
    /// </summary>
    [JsonProperty(PropertyName = "PreviewImageUrl", NullValueHandling = NullValueHandling.Ignore)]
    public string PreviewImageUrl { get; set; }

    public bool RequiresGroupConnected { get; set; }
    public bool RequiresTeamsConnected { get; set; }
    public bool RequiresYammerConnected { get; set; }

    /// <summary>
    /// An array of one or more site scripts. Each is identified by an ID. The scripts will run in the order listed.
    /// </summary>
    [JsonProperty(PropertyName = "SiteScriptIds", NullValueHandling = NullValueHandling.Ignore)]
    public List<Guid> SiteScriptIds { get; set; }

    public string[] SupportedWebTemplates { get; set; }
    public string[] TemplateFeatures { get; set; }
    public string ThumbnailUrl { get; set; }

    /// <summary>
    /// The display name of the site design.
    /// </summary>
    [JsonProperty(PropertyName = "Title", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }

    /// <summary>
    /// Identifies which base template to add the design to. Use the value 64 for the Team site template, and the value 68 for the Communication site template.
    /// </summary>
    [JsonProperty(PropertyName = "WebTemplate", NullValueHandling = NullValueHandling.Ignore)]
    public string WebTemplate { get; set; }

    public string Order { get; set; }

    [JsonProperty(PropertyName = "Version", NullValueHandling = NullValueHandling.Ignore)]
    public int Version { get; set; }
  }

#pragma warning restore CA1056 // Uri properties should not be strings
#pragma warning restore CA2227 // Collection properties should be read only
}
