using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class CreateSiteDesignRequest
	{
		public SiteDesignRequestInfo info { get; set; }

		public CreateSiteDesignRequest(SiteDesignMetadata siteDesignMetadata)
		{
			if (siteDesignMetadata == null)
			{
				throw new ArgumentNullException(nameof(siteDesignMetadata));
			}

			info = new SiteDesignRequestInfo
			{
				Title = siteDesignMetadata.Title,
				Description = siteDesignMetadata.Description,
				SiteScriptIds = siteDesignMetadata.SiteScriptIds,
				WebTemplate = siteDesignMetadata.WebTemplate,
				PreviewImageUrl = siteDesignMetadata.PreviewImageUrl,
				PreviewImageAltText = siteDesignMetadata.PreviewImageAltText,
				ThumbnailUrl = siteDesignMetadata.ThumbnailUrl,
			};

		}
	}
}
