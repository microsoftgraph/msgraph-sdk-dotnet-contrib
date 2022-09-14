namespace Graph.Community
{
  class FieldMultiLineText:Field
  {
    public bool AllowHyperlink { get; set; }
    public bool AppendOnly { get; set; }
    public bool IsLongHyperlink { get; set; }
    public int NumberOfLines { get; set; }
    public bool RestrictedMode { get; set; }
    public bool RichText { get; set; }
    public bool UnlimitedLengthInDocumentLibrary { get; set; }
    public bool WikiLinking { get; set; }
  }
}
