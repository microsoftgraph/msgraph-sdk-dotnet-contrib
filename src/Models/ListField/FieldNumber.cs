namespace Graph.Community
{
  public class FieldNumber : Field
  {
    public bool CommaSeparator { get; set; }
    public string CustomUnitName { get; set; }
    public bool CustomUnitOnRight { get; set; }
    public int DisplayFormat { get; set; }
    public double MaximumValue { get; set; }
    public double MinimumValue { get; set; }
    public bool ShowAsPercentage { get; set; }
    public string Unit { get; set; }
  }
}
