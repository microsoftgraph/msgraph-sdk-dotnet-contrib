using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SitePageVersionCollectionPage>))]
  public interface ISitePageVersionCollectionPage : ICollectionPage<SitePageVersion>
  {
  }
}
