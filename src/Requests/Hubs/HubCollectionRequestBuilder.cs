using System;
using System.Collections.Generic;
using Microsoft.Graph;

namespace Graph.Community
{
  public class HubCollectionRequestBuilder : BaseRequestBuilder, IHubCollectionRequestBuilder
  {
    private readonly IEnumerable<Option> options;

    public HubCollectionRequestBuilder(
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
    public IHubCollectionRequest Request()
    {
      return this.Request(options);
    }

    /// <summary>
    /// Builds the request.
    /// </summary>
    /// <param name="options">The query and header options for the request.</param>
    /// <returns>The built request.</returns>
    public IHubCollectionRequest Request(IEnumerable<Option> options)
    {
      return new HubCollectionRequest(this.AppendSegmentToRequestUrl("HubSites"), this.Client, options);
    }

    /// <summary>
    /// Gets an <see cref="IHubRequestBuilder"/> for the specified Hub
    /// </summary>
    /// <param name="id">The ID for the Hub</param>
    /// <returns>The <see cref="IHubRequestBuilder"/>.</returns>
    public IHubRequestBuilder this[string id]
    {
      get
      {
        if (string.IsNullOrEmpty(id))
        {
          throw new ArgumentNullException(nameof(id));
        }

        List<QueryOption> options = [new("hubSiteId", $"'{id}'")];
        return new HubRequestBuilder(this.AppendSegmentToRequestUrl("HubSites/GetById"), this.Client, options);
      }
    }
  }
}
