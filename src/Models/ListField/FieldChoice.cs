namespace Graph.Community
{
  public class FieldChoice : FieldMultiChoice
  {
    public ChoiceFormatType EditFormat { get; set; }
  }

  public enum ChoiceFormatType
  {
    Dropdown,
    RadioButtons
  }
}
