using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public class FieldLookup : Field
  {
    public bool AllowMultipleValues { get; set; }
    public IList<string> DependentLookupInternalNames { get; set; }
    public bool IsDependentLookup { get; set; }
    public bool IsRelationship { get; set; }
    public string LookupField { get; set; }
    public string LookupList { get; set; }
    public Guid LookupWebId { get; set; }
    public string PrimaryFieldId { get; set; }
    public RelationshipDeleteBehaviorType RelationshipDeleteBehavior { get; set; }
    public bool UnlimitedLengthInDocumentLibrary { get; set; }
  }
  public enum RelationshipDeleteBehaviorType
  {
    None,
    Cascade,
    Restrict
  }
}
