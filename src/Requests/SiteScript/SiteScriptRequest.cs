using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Graph.Community
{
  public class SiteScriptRequest : BaseRequest, ISiteScriptRequest
  {
#pragma warning disable CA1054 // URI parameters should not be strings
    public SiteScriptRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
      this.Method = System.Net.Http.HttpMethod.Post.Method;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

    #region Get

    public Task<ICollectionPage<SiteScriptMetadata>> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<ICollectionPage<SiteScriptMetadata>> GetAsync(CancellationToken cancellationToken)
    {
      GetSiteScriptCollectionResponse response = new GetSiteScriptCollectionResponse();

      if (this.QueryOptions.Any(o => o.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase)))
      {

        // TODO: Create separate requests for Metadata and Collection of metadata

        var idOption = this.QueryOptions.First(o => o.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
        var request = new { id = idOption.Value };
        this.QueryOptions.Remove(idOption);

        this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScriptMetadata");
        this.ContentType = "application/json";
        var entity = await this.SendAsync<SiteScriptMetadata>(request, cancellationToken).ConfigureAwait(false);

        response.Value.Add(entity);
      }
      else
      {
        this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScripts");

        // TODO: use GetCollectionResponse<SiteDesignMetadata>>
        response = await this.SendAsync<GetSiteScriptCollectionResponse>(null, cancellationToken).ConfigureAwait(false);
      }

      if (response != null && response.Value != null && response.Value.CurrentPage != null)
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
#pragma warning disable CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
        throw new ArgumentOutOfRangeException(paramName: nameof(siteScriptMetadata.Title), message: "Title must be provided");
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
#pragma warning restore CA1303 // Do not pass literals as localized parameters
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
