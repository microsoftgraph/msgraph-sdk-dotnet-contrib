using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Graph.Community
{
  public class FieldMultiChoice : Field
  {

    public string[] Choices { get; set; }
    public bool FillInChoice { get; set; }
    public string Mappings { get; set; }
  }
}
