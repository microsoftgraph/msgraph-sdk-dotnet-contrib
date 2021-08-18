using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<SiteUserCollectionPage>))]
  public interface ISiteUserCollectionPage : ICollectionPage<User>
  {
  }
}
