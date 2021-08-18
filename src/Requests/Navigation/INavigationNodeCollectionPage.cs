using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<NavigationNodeCollectionPage>))]
  public interface INavigationNodeCollectionPage : ICollectionPage<NavigationNode>
  {
  }
}
