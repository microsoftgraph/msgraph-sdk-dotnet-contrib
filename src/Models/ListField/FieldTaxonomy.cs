using System;

namespace Graph.Community
{
  public class FieldTaxonomy: Field
  {
    public Guid AnchorId { get; set; }
    public bool CreateValuesInEditForm { get; set; }
    public bool IsAnchorValid { get; set; }
    public bool IsDocTagsEnabled { get; set; }
    public bool IsKeyword { get; set; }
    public bool IsPathRendered { get; set; }
    public bool IsTermSetValid { get; set; }
    public bool Open { get; set; }
    public Guid SspId { get; set; }
    public string TargetTemplate { get; set; }
    public Guid TermSetId { get; set; }
    public Guid TextField { get; set; }
    public bool UserCreated { get; set; }
  }
}
