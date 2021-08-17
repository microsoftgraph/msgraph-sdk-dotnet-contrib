using Microsoft.Graph;

namespace Graph.Community
{
	[InterfaceConverter(typeof(SPInterfaceConverter<ChangeLogCollectionPage>))]
	public interface IChangeLogCollectionPage : ICollectionPage<Change>
	{
	}
}
