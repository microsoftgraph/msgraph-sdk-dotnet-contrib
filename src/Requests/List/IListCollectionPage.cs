using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(InterfaceConverter<ListCollectionPage>))]
  public interface IListCollectionPage:ICollectionPage<List>
  {
  }
}
