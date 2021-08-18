using Microsoft.Graph;
using System;
using System.Collections.Generic;

namespace Graph.Community
{
  class SiteScriptCollectionRequestBuilder : BaseRequestBuilder, ISiteScriptCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public SiteScriptCollectionRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    public ISiteScriptCollectionRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public ISiteScriptCollectionRequest Request(IEnumerable<Option> options)
    {
      return new SiteScriptCollectionRequest(this.RequestUrl, this.Client, options);
    }

    /// <summary>
    /// Gets an <see cref="ISiteDesignCollectionRequestBuilder"/> for the specified SiteDesign.
    /// </summary>
    /// <param name="id">The ID for the SiteDesign.</param>
    /// <returns>The <see cref="ISiteDesignCollectionRequestBuilder"/>.</returns>
    public ISiteScriptRequestBuilder this[string id]
    {
      get
      {
        if (string.IsNullOrEmpty(id))
        {
          throw new ArgumentNullException(nameof(id));
        }

        // the usual model is to append the id to the query
        // Site Scripts require the id in the request body, so put it in options for now
        List<QueryOption> options = new List<QueryOption>() { new QueryOption("id", id.ToString()) };
        return new SiteScriptRequestBuilder(this.RequestUrl, this.Client, options);
      }
    }
  }
}
