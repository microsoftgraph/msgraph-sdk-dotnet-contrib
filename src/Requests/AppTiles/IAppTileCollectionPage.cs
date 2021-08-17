using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	[InterfaceConverter(typeof(SPInterfaceConverter<AppTileCollectionPage>))]
	public interface IAppTileCollectionPage : ICollectionPage<AppTile>
	{
	}
}
