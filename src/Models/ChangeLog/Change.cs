using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Graph.Community
{
  [DebuggerDisplay("{ODataType, nq}")]
  [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
  [JsonConverter(typeof(SPChangeDerivedTypedConverter))]
  public class Change : BaseItem, IChange
  {
    public ChangeToken ChangeToken { get; set; }
    public ChangeType ChangeType { get; set; }
    public Guid SiteId { get; set; }
    public DateTime Time { get; set; }
  }
}
