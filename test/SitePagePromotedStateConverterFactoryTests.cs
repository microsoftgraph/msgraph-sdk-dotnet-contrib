using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class SitePagePromotedStateConverterFactoryTests
  {
    private readonly ITestOutputHelper output;

    public SitePagePromotedStateConverterFactoryTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact(Skip ="No longer needed?")]
    public void DeserializePromotedStateFromDoubleCorrectly()
    {
      // ARRANGE
      var serializedText = "{\"PromotedState\":0.0}";

      // ACT
      var actual = JsonSerializer.Deserialize<SitePage>(serializedText);

      // ASSERT
      Assert.Equal(SitePagePromotedState.NotPromoted, actual.PromotedState);
    }

    [Fact(Skip = "No longer needed?")]
    public void DeserializePromotedStateFromStringCorrectly()
    {
      // ARRANGE
      var serializedText = "{\"PromotedState\":\"NotPromoted\"}";

      // ACT
      var actual = JsonSerializer.Deserialize<SitePage>(serializedText);

      // ASSERT
      Assert.Equal(SitePagePromotedState.NotPromoted, actual.PromotedState);
    }

  }
}
