using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class SiteScriptCollectionRequestBuilder : BaseRequestBuilder, ISiteScriptCollectionRequestBuilder
  {
    private IEnumerable<Option> options;

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
    /// Gets an <see cref="ISiteScriptRequestBuilder"/> for the specified SiteScript.
    /// </summary>
    /// <param name="id">The ID for the SiteScript.</param>
    /// <returns>The <see cref="ISiteScriptRequestBuilder"/>.</returns>
    public ISiteScriptRequestBuilder this[string id]
    {
      get
      {
        if (id == null)
        {
          throw new ArgumentNullException(nameof(id));
        }

        List<QueryOption> options = new List<QueryOption>() { new QueryOption("id", id.ToString()) };

        return new SiteScriptRequestBuilder(this.RequestUrl, this.Client, options);
      }
    }
  }
}
