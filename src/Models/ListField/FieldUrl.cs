namespace Graph.Community
{
  public class FieldUrl:Field
  {
    public UrlFieldFormatType DisplayFormat { get; set; }
  }

  public enum UrlFieldFormatType
  {
    Hyperlink,
    Image
  }
}
