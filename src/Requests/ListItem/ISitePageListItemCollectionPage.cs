using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SitePageListItemCollectionPage>))]
  public interface ISitePageListItemCollectionPage: ICollectionPage<SitePageListItem>
  {
  }
}
