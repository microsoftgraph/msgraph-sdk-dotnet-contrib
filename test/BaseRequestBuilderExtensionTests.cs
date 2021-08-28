using Microsoft.Graph;
using Xunit;

namespace Graph.Community.Test
{
  public class BaseRequestBuilderExtensionTests
  {
    [Fact]
    public void GetResourceSubscriptionPathReturnsCorrectPath()
    {
      // ARRANGE
      var teamId = "01b4b70e-2ea6-432f-a3d7-eefd826c2a8e";
      var channelId = "19:81cf89b7ecef4e7994a84ee2cfb3248a@thread.skype";
      var expectedChannelPath = "/teams/01b4b70e-2ea6-432f-a3d7-eefd826c2a8e/channels/19:81cf89b7ecef4e7994a84ee2cfb3248a@thread.skype";

      var expectedMeMessagesPath = "/me/messages";

      // ACT
      using var gsc = GraphServiceTestClient.Create();
      var channelResource = gsc.GraphServiceClient.Teams[teamId].Channels[channelId];
      var actualChannelPath = (channelResource as IBaseRequestBuilder).GetResourceSubscriptionPath();

      var meMessagesResource = gsc.GraphServiceClient.Me.Messages;
      var actualMeMessagesPath = (meMessagesResource as IBaseRequestBuilder).GetResourceSubscriptionPath();

      // ASSERT
      Assert.Equal(expectedChannelPath, actualChannelPath);
      Assert.Equal(expectedMeMessagesPath, actualMeMessagesPath);
    }

    [Fact]
    public void WithODataCastUpdatesUrl()
    {
      // ARRANGE
      var oDataCast = "microsoft.graph.directoryRole";
      var expectedUrl = "/me/memberOf/microsoft.graph.directoryRole";

      // ACT
      using var gsc = GraphServiceTestClient.Create();
      var request = gsc.GraphServiceClient.Me.MemberOf.WithODataCast(oDataCast).Request();
      var actualUrl = request.RequestUrl;

      // ASSERT
      Assert.True(actualUrl.IndexOf(expectedUrl) > -1, "RequestUrl does not include odata cast  value");
    }
  }
}
