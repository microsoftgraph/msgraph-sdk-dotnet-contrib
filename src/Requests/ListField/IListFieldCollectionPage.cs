using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(SPODataTypeConverter<ListFieldCollectionPage>))]
  public interface IListFieldCollectionPage:ICollectionPage<Field>
  {
  }
}
