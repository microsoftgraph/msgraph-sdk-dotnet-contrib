using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class SiteScriptRequest : BaseSharePointAPIRequest, ISiteScriptRequest
  {
    public SiteScriptRequest(
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base("SiteScript", requestUrl, client, options)
    {
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.AcceptHeaderName, SharePointAPIRequestConstants.Headers.AcceptHeaderValue));
      this.Headers.Add(new HeaderOption(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName, SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue));
      this.Method = HttpMethods.POST;
    }

    #region Get

    public Task<SiteScriptMetadata> GetAsync()
    {
      return this.GetAsync(CancellationToken.None);
    }

    public async Task<SiteScriptMetadata> GetAsync(CancellationToken cancellationToken)
    {
      // the usual model is to append the id to the query
      // Site Designs require the id in the request body, so grab it from options 

      var idOption = this.QueryOptions.First(o => o.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
      this.QueryOptions.Remove(idOption);

      // create the object that must be posted 
      var request = new { id = idOption.Value };

      // still need to update the url, just not with the Id
      this.AppendSegmentToRequestUrl("Microsoft.Sharepoint.Utilities.WebTemplateExtensions.SiteScriptUtility.GetSiteScriptMetadata");

      this.ContentType = "application/json";
      var entity = await this.SendAsync<SiteScriptMetadata>(request, cancellationToken).ConfigureAwait(false);

      return entity;
    }

    #endregion

  }
}
