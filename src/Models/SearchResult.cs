using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class SearchResult
	{
		[JsonProperty("odata.metadata")]
		public Uri OdataMetadata { get; set; }

		[JsonProperty("ElapsedTime")]
		public long ElapsedTime { get; set; }

		[JsonProperty("PrimaryQueryResult")]
		public PrimaryQueryResult PrimaryQueryResult { get; set; }

		[JsonProperty("Properties")]
		public List<object> Properties { get; set; }

		[JsonProperty("SecondaryQueryResults")]
		public List<object> SecondaryQueryResults { get; set; }

		[JsonProperty("SpellingSuggestion")]
		public string SpellingSuggestion { get; set; }

		[JsonProperty("TriggeredRules")]
		public List<object> TriggeredRules { get; set; }
	}

	public class PrimaryQueryResult
	{
		[JsonProperty("CustomResults")]
		public List<object> CustomResults { get; set; }

		[JsonProperty("QueryId")]
		public Guid QueryId { get; set; }

		[JsonProperty("QueryRuleId")]
		public Guid QueryRuleId { get; set; }

		[JsonProperty("RefinementResults")]
		public object RefinementResults { get; set; }

		[JsonProperty("RelevantResults")]
		public RelevantResults RelevantResults { get; set; }

		[JsonProperty("SpecialTermResults")]
		public object SpecialTermResults { get; set; }
	}

	public class RelevantResults
	{
		[JsonProperty("GroupTemplateId")]
		public object GroupTemplateId { get; set; }

		[JsonProperty("ItemTemplateId")]
		public object ItemTemplateId { get; set; }

		[JsonProperty("Properties")]
		public List<ResultKeyValuePair> Properties { get; set; }

		[JsonProperty("ResultTitle")]
		public object ResultTitle { get; set; }

		[JsonProperty("ResultTitleUrl")]
		public object ResultTitleUrl { get; set; }

		[JsonProperty("RowCount")]
		public long RowCount { get; set; }

		[JsonProperty("Table")]
		public Table Table { get; set; }

		[JsonProperty("TotalRows")]
		public long TotalRows { get; set; }

		[JsonProperty("TotalRowsIncludingDuplicates")]
		public long TotalRowsIncludingDuplicates { get; set; }
	}

	public class Table
	{
		[JsonProperty("Rows")]
		public List<Row> Rows { get; set; }
	}

	public class Row
	{
		[JsonProperty("Cells")]
		public List<ResultKeyValuePair> Cells { get; set; }
	}

	public class ResultKeyValuePair
	{
		[JsonProperty("Key")]
		public string Key { get; set; }

		[JsonProperty("Value")]
		public string Value { get; set; }

		[JsonProperty("ValueType")]
		public string ValueType { get; set; }
	}
}
