using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface IChange
  {
    ChangeToken ChangeToken { get; set; }
    ChangeType ChangeType { get; set; }
    Guid SiteId { get; set; }
    DateTime Time { get; set; }
  }
}
