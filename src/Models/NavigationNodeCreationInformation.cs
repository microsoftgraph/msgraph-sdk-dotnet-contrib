using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
  public class NavigationNodeCreationInformation
  {
    public string Title { get; set; }
    public Uri Url { get; set; }
  }
}
