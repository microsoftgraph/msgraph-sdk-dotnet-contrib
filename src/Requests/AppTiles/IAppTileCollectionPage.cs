using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(SPODataTypeConverter<AppTileCollectionPage>))]
  public interface IAppTileCollectionPage : ICollectionPage<AppTile>
  {
  }
}
