using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Graph.Community
{
  [InterfaceConverter(typeof(SPInterfaceConverter<Change>))]
  public interface IChange
  {
    ChangeToken ChangeToken { get; set; }
    ChangeType ChangeType { get; set; }
    Guid SiteId { get; set; }
    DateTime Time { get; set; }
  }
}
