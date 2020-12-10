using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(SPDerivedTypedConverter))]
	public class AppTile : BaseItem
	{
		[JsonProperty]
		public Guid AppId { get; set; }

		[JsonProperty]
		public string AppPrincipalId { get; set; }

		[JsonProperty]
		public AppSource AppSource { get; set; }

		[JsonProperty]
		public AppStatus AppStatus { get; set; }

		[JsonProperty]
		public AppType AppType { get; set; }

		[JsonProperty]
		public string AssetId { get; set; }

		/// <summary>
		/// Microsoft.SharePoint.Client.ListTemplateType
		/// </summary>
		[JsonProperty]
		public int BaseTemplate { get; set; }

		[JsonProperty]
		public int ChildCount { get; set; }

		[JsonProperty]
		public string ContentMarket { get; set; }

		[JsonProperty]
		public string CustomSettingsUrl { get; set; }

		[JsonProperty]
		public new string Description { get; set; }

		[JsonProperty]
		public bool IsCorporateCatalogSite { get; set; }

		[JsonProperty]
		public string LastModified { get; set; }

		[JsonProperty]
		public DateTime LastModifiedDate { get; set; }

		[JsonProperty]
		public Guid ProductId { get; set; }

		[JsonProperty]
		public string Target { get; set; }

		[JsonProperty]
		public string Thumbnail { get; set; }

		[JsonProperty]
		public string Title { get; set; }

		[JsonProperty]
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
