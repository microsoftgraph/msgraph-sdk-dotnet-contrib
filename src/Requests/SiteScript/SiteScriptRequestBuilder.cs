using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class SiteScriptRequestBuilder : BaseRequestBuilder, ISiteScriptRequestBuilder
  {
    private IEnumerable<Option> options;

#pragma warning disable CA1054 // URI parameters should not be strings
    public SiteScriptRequestBuilder(
        string requestUrl,
        IBaseClient client,
        IEnumerable<Option> options = null)
        : base(requestUrl, client)
    {
      this.options = options;
    }
#pragma warning restore CA1054 // URI parameters should not be strings

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <returns>The built request.</returns>
    public ISiteScriptRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public ISiteScriptRequest Request(IEnumerable<Option> options)
    {
      return new SiteScriptRequest(this.RequestUrl, this.Client, options);
    }

    /// <summary>
    /// Gets an <see cref="ISiteDesignRequestBuilder"/> for the specified SiteDesign.
    /// </summary>
    /// <param name="id">The ID for the SiteDesign.</param>
    /// <returns>The <see cref="ISiteDesignRequestBuilder"/>.</returns>
    public ISiteScriptRequestBuilder this[string id]
    {
      get
      {
        if (id == null)
        {
          throw new ArgumentNullException(nameof(id));
        }

#pragma warning disable CA1305 //Specify IFormatProvider
        List<QueryOption> options = new List<QueryOption>() { new QueryOption("id", id.ToString()) };
#pragma warning restore CA1305 //Specify IFormatProvider

        return new SiteScriptRequestBuilder(this.RequestUrl, this.Client, options);
      }
    }
  }
}
