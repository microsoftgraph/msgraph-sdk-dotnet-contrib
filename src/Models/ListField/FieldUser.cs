namespace Graph.Community
{
  public class FieldUser : FieldLookup
  {
    public bool AllowDisplay { get; set; }
    public bool Presence { get; set; }
    public int SelectionGroup { get; set; }
    public FieldUserSelectionMode SelectionMode { get; set; }
    public string UserDisplayOptions { get; set; }
  }

  public enum FieldUserSelectionMode
  {
    PeopleOnly,
    PeopleAndGroups,
    GroupsOnly
  }
}
