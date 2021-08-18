using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class SearchQueryModelTests
  {
    private readonly ITestOutputHelper output;

    private readonly Serializer ser;

    public SearchQueryModelTests(ITestOutputHelper output)
    {
      this.output = output;

      this.ser = new Serializer();
    }

    [Fact]
    public void MissingQuerytext_Throws()
    {
      // ARRANGE
      var qry = new SearchQuery();

      // ACT

      // ASSERT
      Assert.ThrowsAny<ArgumentException>(() =>
      {
        var actual = ser.SerializeObject(qry);
      });
    }

    public static IEnumerable<object[]> GetSearchQueries()
    {
      yield return new object[]
      {
        new SearchQuery("sharepoint"),
        "{\"request\":{\"Querytext\":\"sharepoint\"}}"
      };
      yield return new object[]
      {
        new SearchQuery("sharepoint", null, null, 0, 500, 500),
        "{\"request\":{\"Querytext\":\"sharepoint\",\"StartRow\":0,\"RowLimit\":500,\"RowsPerPage\":500}}"
      };
      yield return new object[]
      {
        new SearchQuery("sharepoint", new List<string> { "Title", "Path" }),
        "{\"request\":{\"Querytext\":\"sharepoint\",\"SelectProperties\":{\"results\":[\"Title\",\"Path\"]}}}"
      };
      yield return new object[]
      {
        new SearchQuery("sharepoint", sortList: new List<SearchQuery.Sort> { { new SearchQuery.Sort{ Property="[docid]", Direction=SearchQuery.SortDirection.Ascending} } }),
        "{\"request\":{\"Querytext\":\"sharepoint\",\"SortList\":{\"results\":[{\"Property\":\"[docid]\",\"Direction\":\"0\"}]}}}"
      };
    }

    [Theory]
    [MemberData(nameof(GetSearchQueries))]
    public void EmptyProperties_SerializesCorrectly(SearchQuery qry, string expectedSerialization)
    {
      // ARRANGE

      // ACT
      var actual = ser.SerializeObject(qry);

      // ASSERT
      Assert.Equal(expectedSerialization, actual);
    }
  }
}
