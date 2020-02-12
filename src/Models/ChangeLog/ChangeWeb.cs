using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
  public class ChangeWeb : Change
  {
    public Guid WebId { get; set; }
  }
}
