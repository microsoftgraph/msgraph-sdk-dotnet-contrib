using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class SiteDesignCollectionRequestBuilder : BaseRequestBuilder, ISiteDesignCollectionRequestBuilder
  {
    private IEnumerable<Option> options;

    public SiteDesignCollectionRequestBuilder(
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
    public ISiteDesignCollectionRequest Request()
    {
      return this.Request(this.options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public ISiteDesignCollectionRequest Request(IEnumerable<Option> options)
    {
      return new SiteDesignCollectionRequest(this.RequestUrl, this.Client, options);
    }

    /// <summary>
    /// Gets an <see cref="ISiteDesignCollectionRequestBuilder"/> for the specified SiteDesign.
    /// </summary>
    /// <param name="id">The ID for the SiteDesign.</param>
    /// <returns>The <see cref="ISiteDesignCollectionRequestBuilder"/>.</returns>
    public ISiteDesignRequestBuilder this[string id]
    {
      get
      {
        if (id == null)
        {
          throw new ArgumentNullException(nameof(id));
        }

        List<QueryOption> options = new List<QueryOption>() { new QueryOption("id", id.ToString()) };


        return new SiteDesignRequestBuilder(this.RequestUrl, this.Client, options);
      }
    }

  }
}
