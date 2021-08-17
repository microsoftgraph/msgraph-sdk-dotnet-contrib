using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class NavigationNode : BaseItem
  {
    public new int Id { get; set; }

    public string Title { get; set; }

    public Uri Url { get; set; }

    public bool IsDocLib { get; set; }

    public bool IsExternal { get; set; }

    public bool IsVisible { get; set; }

    public int ListTemplateType { get; set; }
  }
}
