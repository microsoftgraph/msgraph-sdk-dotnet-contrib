using System;

namespace Graph.Community
{
  public class ChangeList : Change
  {
    public int BaseTemplate { get; set; }
    public bool Hidden { get; set; }
    public Guid ListId { get; set; }
    public string Title { get; set; }
    public Guid WebId { get; set; }
  }
}
