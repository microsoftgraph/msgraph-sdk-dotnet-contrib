using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageListItemCollectionPage : CollectionPage<SitePageListItem>, ISitePageListItemCollectionPage
  {
    public IListItemCollectionRequest NextPageRequest { get; private set; }

    /// <summary>
    /// Initializes the NextPageRequest property.
    /// </summary>
    public void InitializeNextPageRequest(IBaseClient client, string nextPageLinkString)
    {
      if (!string.IsNullOrEmpty(nextPageLinkString))
      {
        this.NextPageRequest = new ListItemCollectionRequest(
            nextPageLinkString,
            client,
            null);
      }
    }

  }
}
