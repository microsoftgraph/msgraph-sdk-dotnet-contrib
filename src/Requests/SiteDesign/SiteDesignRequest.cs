using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SiteDesignRequest : BaseSharePointAPIRequest, ISiteDesignRequest
  {
    public SiteDesignRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SiteDesign", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
      this.Method = HttpMethods.POST;
    }

    public Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest siteDesign)
    {
      throw new NotImplementedException();
    }

    public Task<ApplySiteDesignResponse> ApplyAsync(ApplySiteDesignRequest siteDesign, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<SiteDesignMetadata> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<SiteDesignMetadata> GetAsync(CancellationToken cancellationToken)
    {
      // the usual model is to append the id to the query
      // Site Designs require the id in the request body, so grab it from options 

      var idOption = this.QueryOptions.First(o => o.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
      this.QueryOptions.Remove(idOption);

      // create the object that must be posted 
      var request = new { id = idOption.Value };

      // still need to update the url, just not with the Id
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteDesignMetadata");

      this.ContentType = "application/json";
      var entity = await this.SendAsync<SiteDesignMetadata>(request, cancellationToken).ConfigureAwait(false);

      return entity;
    }

    public Task<SiteDesignMetadata> UpdateAsync(SiteDesignMetadata siteDesignMetadata)
    {
      return this.UpdateAsync(siteDesignMetadata, CancellationToken.None);
    }

    public async Task<SiteDesignMetadata> UpdateAsync(SiteDesignMetadata siteDesignMetadata, CancellationToken cancellationToken)
    {
      if (siteDesignMetadata is null)
      {
        throw new ArgumentNullException(nameof(siteDesignMetadata));
      }

      // the usual model is to append the id to the query
      // Site Designs require the id in the request body, so grab it from options 

      var idOption = this.QueryOptions.FirstOrDefault(o => o.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
      if (idOption == null)
      {
        throw new ArgumentNullException("Id");
      }

      // if the id used in the request builder differs from what they passed to the method, throw
      var builderId = idOption.Value;
      if (!string.IsNullOrEmpty(siteDesignMetadata.Id) && builderId != siteDesignMetadata.Id)
      {
        throw new ArgumentOutOfRangeException("Id", "The id passed as part of the metadata does not match the id in the request builder");
      }

      this.QueryOptions.Remove(idOption);

      // create the object that must be posted 
      var request = new UpdateSiteDesignRequest(builderId, siteDesignMetadata);

      // still need to update the url, just not with the Id
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.UpdateSiteDesign");

      this.ContentType = "application/json";
      var entity = await this.SendAsync<SiteDesignMetadata>(request, cancellationToken).ConfigureAwait(false);

      return entity;
    }

    #region Delete

    public Task DeleteAsync()
    {
      return this.DeleteAsync(CancellationToken.None);
    }

    public async Task DeleteAsync(CancellationToken cancellationToken)
    {
      // the usual model is to append the id to the query
      // Site Designs require the id in the request body, so grab it from options 

      var idOption = this.QueryOptions.First(o => o.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
      this.QueryOptions.Remove(idOption);

      if (string.IsNullOrEmpty(idOption.Value))
      {
        throw new ArgumentNullException("siteDesignId");
      }

      // create the object that must be posted 
      var request = new { id = idOption.Value };

      // still need to update the url, just not with the Id
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.DeleteSiteDesign");

      this.ContentType = "application/json";
      await this.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    #endregion

  }
}
