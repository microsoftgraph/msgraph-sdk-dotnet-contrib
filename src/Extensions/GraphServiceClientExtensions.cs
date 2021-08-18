using Microsoft.Graph;

namespace Graph.Community
{
  public static class GraphServiceClientExtensions
  {
    public static ISharePointAPIRequestBuilder SharePointAPI(this GraphServiceClient graphServiceClient, string siteUrl)
    {
      return new SharePointAPIRequestBuilder(siteUrl, graphServiceClient);
    }
  }
}
