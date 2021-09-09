using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class SiteDesignMetadata : BaseItem
  {
    [JsonProperty(PropertyName = "DesignPackageId", NullValueHandling = NullValueHandling.Ignore)]
    public Guid DesignPackageId { get; set; }

    [JsonProperty(PropertyName = "DesignType", NullValueHandling = NullValueHandling.Ignore)]
    public string DesignType { get; set; }

    /// <summary>
    /// (Optional) True if the site design is applied as the default site design; otherwise, false. For more information see Customize a default site design.
    /// </summary>
    [JsonProperty(PropertyName = "IsDefault", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsDefault { get; set; }

    [JsonProperty(PropertyName = "IsOutOfBoxTemplate", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsOutOfBoxTemplate { get; set; }

    [JsonProperty(PropertyName = "IsTenantAdminOnly", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool IsTenantAdminOnly { get; set; }

    [JsonProperty(PropertyName = "ListColor", NullValueHandling = NullValueHandling.Ignore)]
    public string ListColor { get; set; }

    [JsonProperty(PropertyName = "ListIcon", NullValueHandling = NullValueHandling.Ignore)]
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

    [JsonProperty(PropertyName = "RequiresGroupConnected", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool RequiresGroupConnected { get; set; }

    [JsonProperty(PropertyName = "RequiresTeamsConnected", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool RequiresTeamsConnected { get; set; }

    [JsonProperty(PropertyName = "RequiresYammerConnected", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool RequiresYammerConnected { get; set; }

    /// <summary>
    /// An array of one or more site scripts. Each is identified by an ID. The scripts will run in the order listed.
    /// </summary>
    [JsonProperty(PropertyName = "SiteScriptIds", NullValueHandling = NullValueHandling.Ignore)]
    public List<Guid> SiteScriptIds { get; set; }

    [JsonProperty(PropertyName = "SupportedWebTemplates", NullValueHandling = NullValueHandling.Ignore)]
    public string[] SupportedWebTemplates { get; set; }

    [JsonProperty(PropertyName = "TemplateFeatures", NullValueHandling = NullValueHandling.Ignore)]
    public string[] TemplateFeatures { get; set; }

    [JsonProperty(PropertyName = "ThumbnailUrl", NullValueHandling = NullValueHandling.Ignore)]
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

    [JsonProperty(PropertyName = "Order", NullValueHandling = NullValueHandling.Ignore)]
    public string Order { get; set; }

    [JsonProperty(PropertyName = "Version", NullValueHandling = NullValueHandling.Ignore)]
    public int Version { get; set; }
  }
}
