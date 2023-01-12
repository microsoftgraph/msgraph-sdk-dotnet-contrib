using System;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class SitePageFileInfoConverterTests
  {
    private readonly ITestOutputHelper output;

    public SitePageFileInfoConverterTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void DeserializesFromBaseEntity()
    {
      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePageResponse.json");
      using (GraphServiceTestClient gsc = GraphServiceTestClient.Create())
      {
        var testPage = gsc.GraphServiceClient.HttpProvider.Serializer.DeserializeObject<SitePageFileInfo>(responseContent);

        // ASSERT
        Assert.IsType<SitePageFileInfo>(testPage);
        AssertSitePageFileInfoProps(testPage);
      }
    }

    [Fact]
    public void DeserializesFromSitePageFileInfo()
    {
      var responseContent = ResourceManager.GetHttpResponseContent("GetSitePageResponse.json");
      using (GraphServiceTestClient gsc = GraphServiceTestClient.Create())
      {
        var x = gsc.GraphServiceClient.HttpProvider.Serializer.DeserializeObject<SitePageFileInfo>(responseContent);
        var y = JsonSerializer.Serialize(x);
        var testPage = JsonSerializer.Deserialize<SitePageFileInfo>(y);
        AssertSitePageFileInfoProps(testPage);
      }
    }

    private void AssertSitePageFileInfoProps(SitePageFileInfo testPage)
    {
      Assert.Equal(6, testPage.Id);
      Assert.Equal("Champions", testPage.Title);
      Assert.StartsWith("Make a difference ", testPage.Description);
      Assert.Equal(SitePagePromotedState.NotPromoted, testPage.PromotedState);
      Assert.Equal(new DateTimeOffset(2022, 5, 25, 16, 07, 42, new TimeSpan()), testPage.FirstPublishedDateTime);
      Assert.Equal(new DateTimeOffset(2021, 9, 10, 15, 11, 28, new TimeSpan()), testPage.LastModifiedDateTime);
      Assert.Equal("champions.aspx", testPage.FileName);
      Assert.Equal("cef16a53-9b15-44f2-898e-ba67b9ada101", testPage.UniqueId);
      Assert.Equal(2, testPage.ModernAudienceTargetUsers.Count);
      Assert.Equal(new DateTimeOffset(2021, 6, 24, 12, 29, 16, new TimeSpan()), testPage.CreatedDateTime);
      Assert.IsType<Graph.Community.UserInfo>(testPage.Author);
      Assert.Equal(6, testPage.Author.Id);
      Assert.IsType<Graph.Community.UserInfo>(testPage.Editor);
      Assert.Equal(17, testPage.Editor.Id);
      Assert.Equal(SitePageCheckoutType.None, testPage.CheckoutType);
      Assert.Equal(6, testPage.CheckoutUser.Id);
      Assert.Equal("/sites/mockSite/SitePages/champions.aspx", testPage.ServerRelativeUrl);
    }
  }
}
