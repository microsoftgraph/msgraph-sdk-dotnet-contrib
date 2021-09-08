using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteDesignCollectionRequest : BaseSharePointAPIRequest, ISiteDesignCollectionRequest
  {
    public SiteDesignCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SiteDesign", requestUrl, client, options)
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
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns");
      var response = await this.SendAsync<GetCollectionResponse<SiteDesignMetadata>>(null, cancellationToken).ConfigureAwait(false); 

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
      if (siteDesignMetadata == null)
      {
        throw new ArgumentNullException(nameof(siteDesignMetadata));
      }

      if (string.IsNullOrEmpty(siteDesignMetadata.Title))
      {
        throw new ArgumentException(paramName: nameof(siteDesignMetadata.Title), message: "Title must be provided");
      }
      if (siteDesignMetadata.SiteScriptIds == null ||
          siteDesignMetadata.SiteScriptIds.Count == 0)
      {
        throw new ArgumentException(paramName: nameof(siteDesignMetadata.SiteScriptIds), message: "Site Script Id(s) must be provided");
      }
      if (string.IsNullOrEmpty(siteDesignMetadata.WebTemplate))
      {
        throw new ArgumentException(paramName: nameof(siteDesignMetadata.WebTemplate), message: "Web Template must be provided");
      }

      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteDesign");
      this.ContentType = "application/json";
      var requestData = new CreateSiteDesignRequest(siteDesignMetadata);
      var newEntity = await this.SendAsync<SiteDesignMetadata>(requestData, cancellationToken).ConfigureAwait(false);
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
