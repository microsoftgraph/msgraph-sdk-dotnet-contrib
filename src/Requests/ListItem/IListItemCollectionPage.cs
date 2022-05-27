using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<ListItemCollectionPage>))]
  public interface IListItemCollectionPage:ICollectionPage<ListItem>
  {
  }
}
