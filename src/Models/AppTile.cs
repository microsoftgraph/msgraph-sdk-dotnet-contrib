using System;
using System.Text.Json.Serialization;
using Microsoft.Graph;

namespace Graph.Community
{
  public class AppTile : BaseItem
  {
    public Guid AppId { get; set; }

    [JsonPropertyName("AppPrincipalId")]
    public string AppPrincipalId { get; set; }

    public AppSource AppSource { get; set; }

    public AppStatus AppStatus { get; set; }

    public AppType AppType { get; set; }

    public string AssetId { get; set; }

    /// <summary>
    /// Microsoft.SharePoint.Client.ListTemplateType
    /// </summary>
    public int BaseTemplate { get; set; }

    public int ChildCount { get; set; }

    public string ContentMarket { get; set; }

    public string CustomSettingsUrl { get; set; }

    public new string Description { get; set; }

    public bool IsCorporateCatalogSite { get; set; }

    public string LastModified { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public Guid ProductId { get; set; }

    public string Target { get; set; }

    public string Thumbnail { get; set; }

    public string Title { get; set; }

    public string Version { get; set; }
  }

  public enum AppSource
  {
    InvalidSource,
    Marketplace,
    CorporateCatalog,
    DeveloperSite,
    ObjectModel,
    RemoteObjectModel,
    SiteCollectionCorporateCatalog
  }

  public enum AppStatus
  {
    InvalidStatus,
    Installing,
    Canceling,
    Uninstalling,
    Installed,
    Upgrading,
    Initialized,
    UpgradeCanceling,
    Disabling,
    Disabled,
    SecretRolling,
    Recycling,
    Recycled,
    Restoring,
    RestoreCanceling
  }

  public enum AppType
  {
    Doclib,
    List,
    Tenant,
    Instance,
    Feature,
    CommonList
  }
}
