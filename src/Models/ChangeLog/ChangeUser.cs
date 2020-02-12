using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
  public class ChangeUser : Change
  {
    public bool Activate { get; set; }
    public int UserId { get; set; }

  }
}
