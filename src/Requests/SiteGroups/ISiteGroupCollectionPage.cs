using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SiteGroupCollectionPage>))]
  public interface ISiteGroupCollectionPage : ICollectionPage<Group>
  {
  }
}
