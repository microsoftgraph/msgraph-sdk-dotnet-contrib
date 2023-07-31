using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  [JsonConverter(typeof(SPSearchQueryConverter))]
  public class SearchQuery
  {
    public SearchQuery()
    {
      this.Request = new RequestProperties();
    }

    public SearchQuery(string queryText, List<string> selectProperties = null, List<Sort> sortList = null, long? startRow = null, int? rowLimit = null, int? rowsPerPage = null, bool? trimDuplicates = null)
      : this()
    {
      this.Request.Querytext = queryText;
      this.Request.StartRow = startRow;
      this.Request.RowLimit = rowLimit;
      this.Request.RowsPerPage = rowsPerPage;

      if (selectProperties != null)
      {
        this.Request.SelectProperties.PropertyList.AddRange(selectProperties);
      }

      if (sortList != null)
      {
        this.Request.SortList.SortProperties.AddRange(sortList);
      }

      this.Request.TrimDuplicates = trimDuplicates;
    }

    [JsonPropertyName("request")]
    public RequestProperties Request { get; set; }

    public class RequestProperties
    {
      [JsonPropertyName("Querytext")]
      public string Querytext { get; set; }
      public bool ShouldSerializeQuerytext() => (string.IsNullOrEmpty(this.Querytext)) ? throw new ArgumentException("Querytext must be provided") : true;

      public SelectProperties SelectProperties { get; set; }
      public bool ShouldSerializeSelectProperties() => this.SelectProperties.PropertyList.Count > 0;

      public SortList SortList { get; set; }
      public bool ShouldSerializeSortList() => this.SortList.SortProperties.Count > 0;

      public long? StartRow { get; set; }
      public bool ShouldSerializeStartRow() => this.StartRow.HasValue;

      public int? RowLimit { get; set; }
      public bool ShouldSerializeRowLimit() => this.RowLimit.HasValue;

      public int? RowsPerPage { get; set; }
      public bool ShouldSerializeRowsPerPage() => this.RowsPerPage.HasValue;

      public bool? TrimDuplicates { get; set; }

      public RequestProperties()
      {
        this.SelectProperties = new SelectProperties();
        this.SortList = new SortList();
      }
    }

    public class SelectProperties
    {
      [JsonPropertyName("results")]
      public List<string> PropertyList { get; set; }

      public SelectProperties()
      {
        this.PropertyList = new List<string>();
      }
    }

    public class SortList
    {
      [JsonPropertyName("results")]
      public List<Sort> SortProperties { get; set; }

      public void Add(string property, SortDirection direction)
      {
        if (!this.SortProperties.Exists(s => s.Property.ToLowerInvariant() == property.ToLowerInvariant()))
        {
          this.SortProperties.Add(new Sort { Property = property, Direction = direction });
        }
      }

      public SortList()
      {
        this.SortProperties = new List<Sort>();
      }
    }

    public class Sort
    {
      public Sort() { }
      public Sort(string property, SortDirection direction)
      {
        this.Property = property;
        this.Direction = direction;
      }

      public string Property { get; set; }

      [JsonIgnore]
      public SortDirection Direction
      {
        get
        {
          return direction;
        }
        set
        {
          direction = value;
          directionAsString = ((int)value).ToString();
        }
      }
      private SortDirection direction;

      [JsonPropertyName("direction")]
      internal string directionAsString;
    }

    // Borrowed from Microsoft.SharePoint.Client.Search.Query
    public enum SortDirection
    {
      Ascending,
      Descending
    }
  }
}
