using Microsoft.Graph;

namespace Graph.Community
{
  public class List : BaseItem
  {
    public new string Id { get; set; }
    public string Title { get; set; }
    public int BaseTemplate { get; set; }
    public System.Collections.Generic.List<Form> Forms { get; set; }
    public ChangeToken CurrentChangeToken { get; set; }
  }
}
