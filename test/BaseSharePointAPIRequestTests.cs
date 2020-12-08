using Graph.Community.Test.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class BaseSharePointAPIRequestTests
  {
    private readonly ITestOutputHelper output;

    public BaseSharePointAPIRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task SetsDefaultHandlerOption()
    {
      // ARRANGE
      var handler = new MockSharePointServiceHandler();
      var client = GraphServiceTestClient.Create(handler);
      var req = new BaseSharePointAPIRequest("TEST", "http://localhost", client.GraphServiceClient, null);

      // ACT
      await req.SendAsync(null, CancellationToken.None);

      // ASSERT
      Assert.True(handler.HasOptions);
    }

    [Fact]
    public async Task SetsHandlerOption()
    {
      // ARRANGE
      var handler = new MockSharePointServiceHandler();
      var client = GraphServiceTestClient.Create(handler);
      var expected = true;
      var req = new BaseSharePointAPIRequest("TEST", "http://localhost", client.GraphServiceClient, null);

      // ACT
      await req.WithTelemetryDisabled().SendAsync(null, CancellationToken.None);

      // ASSERT
      Assert.Equal(expected, handler.Options.DisableTelemetry);
      Assert.Equal("TEST", handler.Options.ResourceUri);
    }

    [Fact]
    public async Task SetsHandlerOptionToClientSetting()
    {
      // ARRANGE
      var handler = new MockSharePointServiceHandler();
      var client = GraphServiceTestClient.Create(handler);
      var expected = true;
      CommunityGraphClientFactory.TelemetryDisabled = expected;
      var req = new BaseSharePointAPIRequest("TEST", "http://localhost", client.GraphServiceClient, null);

      // ACT
      await req.SendAsync(null, CancellationToken.None);

      // ASSERT
      Assert.Equal(expected, handler.Options.DisableTelemetry);
    }
  }
}
