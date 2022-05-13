using System;
using System.Diagnostics;
using Microsoft.Graph;

namespace Graph.Community
{
  [DebuggerDisplay("{ODataType, nq}")]
  [SPDerivedTypeConverter(typeof(SPODataTypeConverter<Group>))]
  public class Change : BaseItem
  {
    public ChangeToken ChangeToken { get; set; }
    public ChangeType ChangeType { get; set; }
    public Guid SiteId { get; set; }
    public DateTime Time { get; set; }

    public Change()
      : base()
    {
    }
  }
}
