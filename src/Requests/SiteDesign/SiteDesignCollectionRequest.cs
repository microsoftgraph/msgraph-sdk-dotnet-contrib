using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SiteDesignCollectionRequest : BaseSharePointAPIRequest, ISiteDesignCollectionRequest
  {
    //_api/Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteDesign

    public SiteDesignCollectionRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SiteDesign", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
      this.Method = HttpMethods.POST;
    }

    #region Get

    public Task<ISiteDesignCollectionPage> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<ISiteDesignCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesigns");

      var response = await this.SendAsync<SharePointAPICollectionResponse<ISiteDesignCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
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

    public Task<IApplySiteDesignActionOutcomeCollectionPage> ApplyAsync(ApplySiteDesignRequest site)
    {
      return this.ApplyAsync(site, CancellationToken.None);
    }

    public async Task<IApplySiteDesignActionOutcomeCollectionPage> ApplyAsync(ApplySiteDesignRequest siteDesign, CancellationToken cancellationToken)
    {
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.ApplySiteDesign");
      this.ContentType = "application/json";
      var response = await this.SendAsync<SharePointAPICollectionResponse<IApplySiteDesignActionOutcomeCollectionPage>>(siteDesign, cancellationToken);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }

    #endregion
  }
}
