using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SitePageCollectionPage>))]
  public interface ISitePageCollectionPage: ICollectionPage<SitePage>
  {
  }
}
