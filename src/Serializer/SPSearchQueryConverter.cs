using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SPSearchQueryConverter : JsonConverter<SearchQuery>
  {
    public override SearchQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, SearchQuery value, JsonSerializerOptions options)
    {
      if (string.IsNullOrEmpty(value.Request.Querytext))
      {
        throw new ArgumentException("Querytext must be provided");
      }

      dynamic requestObjectToSerialize = new ExpandoObject();
      requestObjectToSerialize.Querytext = value.Request.Querytext;

      if (value.Request.SelectProperties.PropertyList.Count > 0)
      {
        requestObjectToSerialize.SelectProperties = new { results = value.Request.SelectProperties.PropertyList };
      }

      if (value.Request.SortList.SortProperties.Count > 0)
      {
        List<dynamic> sortList = new List<dynamic>();
        foreach (var sortProperty in value.Request.SortList.SortProperties)
        {
          sortList.Add(new { Property = sortProperty.Property, Direction = sortProperty.directionAsString });
        }
        requestObjectToSerialize.SortList = new { results = sortList };
      }
      if (value.Request.StartRow.HasValue)
      {
        requestObjectToSerialize.StartRow = value.Request.StartRow.Value;
      }
      if (value.Request.RowLimit.HasValue)
      {
        requestObjectToSerialize.RowLimit = value.Request.RowLimit.Value;
      }
      if (value.Request.RowsPerPage.HasValue)
      {
        requestObjectToSerialize.RowsPerPage = value.Request.RowsPerPage.Value;
      }
      if (value.Request.TrimDuplicates.HasValue)
      {
        requestObjectToSerialize.TrimDuplicates = value.Request.TrimDuplicates.Value;
      }

      var objectToSerialize = new
      {
        request = requestObjectToSerialize
      };

      // Don't pass in options when recursively calling Serialize.
      JsonSerializer.Serialize(writer, objectToSerialize);

    }
  }
}
