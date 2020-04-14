using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
#pragma warning disable CA1056 // Uri properties should not be strings
#pragma warning disable CA2227 // Collection properties should be read only

  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class SiteDesignMetadata : BaseItem
  {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "DesignPackageId", Required = Newtonsoft.Json.Required.Default)]
    public Guid DesignPackageId { get; set; }

    /// <summary>
    /// (Optional) True if the site design is applied as the default site design; otherwise, false. For more information see Customize a default site design.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "IsDefault", Required = Newtonsoft.Json.Required.Default)]
    public bool IsDefault { get; set; }

    /// <summary>
    /// (Optional) The alt text description of the image for accessibility.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "PreviewImageAltText", Required = Newtonsoft.Json.Required.Default)]
    public string PreviewImageAltText { get; set; }

    /// <summary>
    /// (Optional) The URL of a preview image. If none is specified, SharePoint uses a generic image.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "PreviewImageUrl", Required = Newtonsoft.Json.Required.Default)]
    public string PreviewImageUrl { get; set; }

    /// <summary>
    /// An array of one or more site scripts. Each is identified by an ID. The scripts will run in the order listed.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SiteScriptIds", Required = Newtonsoft.Json.Required.Default)]
    public List<Guid> SiteScriptIds { get; set; }

    /// <summary>
    /// The display name of the site design.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Title", Required = Newtonsoft.Json.Required.Default)]
    public string Title { get; set; }

    /// <summary>
    /// Identifies which base template to add the design to. Use the value 64 for the Team site template, and the value 68 for the Communication site template.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "WebTemplate", Required = Newtonsoft.Json.Required.Default)]
    public string WebTemplate { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Version", Required = Newtonsoft.Json.Required.Default)]
    public int Version { get; set; }
  }

#pragma warning restore CA1056 // Uri properties should not be strings
#pragma warning restore CA2227 // Collection properties should be read only
}
