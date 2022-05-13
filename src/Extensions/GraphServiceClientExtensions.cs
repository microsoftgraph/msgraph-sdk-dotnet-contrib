using System;
using Microsoft.Graph;

namespace Graph.Community
{
  public static class GraphServiceClientExtensions
  {
    public static ISharePointAPIRequestBuilder SharePointAPI(this GraphServiceClient graphServiceClient, string siteUrl)
    {
      if (graphServiceClient is null)
      {
        throw new ArgumentNullException(nameof(graphServiceClient));
      }

      if (string.IsNullOrEmpty(siteUrl))
      {
        throw new System.ArgumentException($"'{nameof(siteUrl)}' cannot be null or empty.", nameof(siteUrl));
      }

      return new SharePointAPIRequestBuilder(siteUrl, graphServiceClient);
    }
  }
}
