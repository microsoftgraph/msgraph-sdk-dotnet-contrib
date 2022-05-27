using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class SitePageConverterTests
  {
    private readonly ITestOutputHelper output;

    public SitePageConverterTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void DeserializesFromBaseEntity()
    {
      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePagesResponse.json");
      using (GraphServiceTestClient gsc = GraphServiceTestClient.Create())
      {
        var pages = gsc.GraphServiceClient.HttpProvider.Serializer.DeserializeObject<SharePointAPICollectionResponse<ISitePageCollectionPage>>(responseContent);
        var testPage = pages.Value.CurrentPage[1];

        // ASSERT
        Assert.IsType<SitePage>(testPage);
        AssertSitePageProps(testPage);
      }
    }

    [Fact]
    public void DeserializesFromSitePage()
    {
      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePagesResponse.json");
      using (GraphServiceTestClient gsc = GraphServiceTestClient.Create())
      {
        var x = gsc.GraphServiceClient.HttpProvider.Serializer.DeserializeObject<SharePointAPICollectionResponse<ISitePageCollectionPage>>(responseContent);
        var y = JsonSerializer.Serialize(x.Value.CurrentPage);
        var testPage = JsonSerializer.Deserialize<List<SitePage>>(y);
        AssertSitePageProps(testPage[1]);
      }
    }

    private void AssertSitePageProps(SitePage testPage)
    {
      Assert.IsType<Graph.Community.SitePage>(testPage);
      Assert.Equal(6, testPage.Id);
      Assert.Equal("Champions", testPage.Title);
      Assert.StartsWith("Make a difference ", testPage.Description);
      Assert.Equal(SitePagePromotedState.NotPromoted, testPage.PromotedState);
      Assert.Null(testPage.FirstPublishedDate);
      Assert.Equal(new DateTimeOffset(2021, 9, 10, 15, 11, 28, new TimeSpan()), testPage.LastModifiedDateTime);
      Assert.Equal("champions.aspx", testPage.FileName);
      Assert.Equal("https://mock.sharepoint.com/sites/mockSite/SitePages/champions.aspx", testPage.AbsoluteUrl);
      Assert.Equal("SitePages/champions.aspx", testPage.Url);
      Assert.Equal("cef16a53-9b15-44f2-898e-ba67b9ada101", testPage.UniqueId);
    }
  }
}

