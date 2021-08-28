using System;

namespace Graph.Community
{
  public class ChangeItem : Change
  {
    public int ItemId { get; set; }
    public string Editor { get; set; }
    public string EditorEmailHint { get; set; }
    public Guid ListId { get; set; }
    public Guid UniqueId { get; set; }
    public Guid WebId { get; set; }
  }
}
