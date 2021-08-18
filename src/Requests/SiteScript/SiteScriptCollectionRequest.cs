using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Graph.Community
{
  public class SiteScriptCollectionRequest : BaseSharePointAPIRequest, ISiteScriptCollectionRequest
  {
    public SiteScriptCollectionRequest(
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

    public Task<ISiteScriptCollectionPage> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<ISiteScriptCollectionPage> GetAsync(CancellationToken cancellationToken)
    {
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScripts");

      var response = await this.SendAsync<SharePointAPICollectionResponse<ISiteScriptCollectionPage>>(null, cancellationToken).ConfigureAwait(false);

      if (response?.Value?.CurrentPage != null)
      {
        return response.Value;
      }

      return null;
    }

    #endregion

    #region Create

    public Task<SiteScriptMetadata> CreateAsync(SiteScriptMetadata siteScriptMetadata)
    {
      return this.CreateAsync(siteScriptMetadata, CancellationToken.None);
    }

    public async Task<SiteScriptMetadata> CreateAsync(SiteScriptMetadata siteScriptMetadata, CancellationToken cancellationToken)
    {
      if (siteScriptMetadata == null)
      {
        throw new ArgumentNullException(nameof(siteScriptMetadata));
      }

      if (string.IsNullOrEmpty(siteScriptMetadata.Title))
      {
        throw new ArgumentOutOfRangeException(paramName: nameof(siteScriptMetadata.Title), message: "Title must be provided");
      }

      var title = HttpUtility.UrlEncode(siteScriptMetadata.Title);
      var desc = HttpUtility.UrlEncode(siteScriptMetadata.Description ?? string.Empty);

      var segment = $"Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.CreateSiteScript(Title=@title,Description=@description)?@title='{title}'&@description='{desc}'";
      this.AppendSegmentToRequestUrl(segment);

      this.ContentType = "application/json";
      var newEntity = await this.SendAsync<SiteScriptMetadata>(siteScriptMetadata.Content, cancellationToken).ConfigureAwait(false);
      return newEntity;
    }

    #endregion
  }
}
