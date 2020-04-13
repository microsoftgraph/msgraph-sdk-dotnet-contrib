using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
  public class ChangeList : Change
  {
    public int BaseTemplate { get; set; }
    public bool Hidden { get; set; }
    public Guid ListId { get; set; }
    public string Title { get; set; }
    public Guid WebId { get; set; }
  }
}
