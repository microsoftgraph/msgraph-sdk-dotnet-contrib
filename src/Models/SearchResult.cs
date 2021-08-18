using System;
using System.Collections.Generic;

namespace Graph.Community
{
  public class SearchResult
  {
    public Uri OdataMetadata { get; set; }

    public long ElapsedTime { get; set; }

    public PrimaryQueryResult PrimaryQueryResult { get; set; }

    public List<object> Properties { get; set; }

    public List<object> SecondaryQueryResults { get; set; }

    public string SpellingSuggestion { get; set; }

    public List<object> TriggeredRules { get; set; }
  }

  public class PrimaryQueryResult
  {
    public List<object> CustomResults { get; set; }

    public Guid QueryId { get; set; }

    public Guid QueryRuleId { get; set; }

    public object RefinementResults { get; set; }

    public RelevantResults RelevantResults { get; set; }

    public object SpecialTermResults { get; set; }
  }

  public class RelevantResults
  {
    public object GroupTemplateId { get; set; }

    public object ItemTemplateId { get; set; }

    public List<ResultKeyValuePair> Properties { get; set; }

    public object ResultTitle { get; set; }

    public object ResultTitleUrl { get; set; }

    public long RowCount { get; set; }

    public Table Table { get; set; }

    public long TotalRows { get; set; }

    public long TotalRowsIncludingDuplicates { get; set; }
  }

  public class Table
  {
    public List<Row> Rows { get; set; }
  }

  public class Row
  {
    public List<ResultKeyValuePair> Cells { get; set; }
  }

  public class ResultKeyValuePair
  {
    public string Key { get; set; }

    public string Value { get; set; }

    public string ValueType { get; set; }
  }
}
