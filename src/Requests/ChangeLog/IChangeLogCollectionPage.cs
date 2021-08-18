using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(SPODataTypeConverter<ChangeLogCollectionPage>))]
  public interface IChangeLogCollectionPage : ICollectionPage<Change>
  {
  }
}
