using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageCollectionPage : CollectionPage<SitePage>, ISitePageCollectionPage
  {
    public ISitePageCollectionRequest NextPageRequest { get; private set; }

    /// <summary>
    /// Initializes the NextPageRequest property.
    /// </summary>
    public void InitializeNextPageRequest(IBaseClient client, string nextPageLinkString)
    {
      if (!string.IsNullOrEmpty(nextPageLinkString))
      {
        this.NextPageRequest = new SitePageCollectionRequest(
            nextPageLinkString,
            client,
            null);
      }
    }

  }
}
