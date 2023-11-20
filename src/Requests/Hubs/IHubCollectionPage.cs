using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<HubCollectionPage>))]
  public interface IHubCollectionPage:ICollectionPage<Hub>
  {
  }
}
