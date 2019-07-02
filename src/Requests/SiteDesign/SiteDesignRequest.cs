using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
	public class SiteDesignRequest : BaseRequest, ISiteDesignRequest
	{
		//_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteDesign
		//_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteDesign

		public SiteDesignRequest(
						string requestUrl,
						IBaseClient client,
						IEnumerable<Option> options)
						: base(requestUrl, client, options)
		{
			
			this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
			this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
			this.Method = System.Net.Http.HttpMethod.Post.Method;
		}

		#region Get

		public Task<ICollectionPage<SiteDesignMetadata>> GetAsync()
		{
			return this.GetAsync(CancellationToken.None);
		}

		public async Task<ICollectionPage<SiteDesignMetadata>> GetAsync(CancellationToken cancellationToken)
		{
			GetSiteDesignCollectionResponse response = new GetSiteDesignCollectionResponse();

			if (this.QueryOptions.Any(o => o.Name.Equals("id")))
			{
				var idOption = this.QueryOptions.First(o => o.Name.Equals("id"));
				var request = new { id = idOption.Value };
				this.QueryOptions.Remove(idOption);

				this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesignMetadata");
				this.ContentType = "application/json";
				var entity = await this.SendAsync<SiteDesignMetadata>(request, cancellationToken).ConfigureAwait(false);

				response.Value = new CollectionPage<SiteDesignMetadata>
				{
					entity
				};
			}
			else
			{
				this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns");
				response = await this.SendAsync<GetSiteDesignCollectionResponse>(null, cancellationToken).ConfigureAwait(false);
			}

			if (response != null && response.Value != null && response.Value.CurrentPage != null)
			{
				return response.Value;
			}

			return null;
		}

		#endregion

		#region Create

		public Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata)
		{
			return this.CreateAsync(siteDesignMetadata, CancellationToken.None);
		}

		public async Task<SiteDesignMetadata> CreateAsync(SiteDesignMetadata siteDesignMetadata, CancellationToken cancellationToken)
		{
			this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteDesign");
			this.ContentType = "application/json";
			var newEntity = await this.SendAsync<SiteDesignMetadata>(siteDesignMetadata, cancellationToken).ConfigureAwait(false);
			return newEntity;
		}

		#endregion

		#region ApplySiteDesign

		public Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest site)
		{
			return this.ApplyAsync(site, CancellationToken.None);
		}

		public Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest siteDesign, CancellationToken cancellationToken)
		{
			this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ApplySiteDesign");
			this.ContentType = "application/json";
			return this.SendAsync<ApplySiteDesignResponse>(siteDesign, cancellationToken);
		}

		#endregion
	}
}