using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class LoggingMessageHandlerTests
  {
    private readonly ITestOutputHelper output;

    public LoggingMessageHandlerTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task WritesToLogger()
    {
      // ARRANGE
      var logger = new StringBuilderHttpMessageLogger();

      var nullHandler = new Mock<HttpClientHandler>();
      nullHandler.Protected()
        .Setup<Task<HttpResponseMessage>>(
          "SendAsync",
          ItExpr.IsAny<HttpRequestMessage>(),
          ItExpr.IsAny<CancellationToken>()
        )
        // prepare the expected response of the mocked http call
        .ReturnsAsync(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK })
        .Verifiable();


      var handler = new LoggingMessageHandler(logger, nullHandler.Object);

      using (var client = new HttpClient(handler, true))
      {
        // ACT
        _ = await client.GetAsync("http://localhost");
        var log = logger.GetLog(true);

        // ASSERT
        Assert.False(string.IsNullOrEmpty(log));
      }

    }

  }

  class StringBuilderHttpMessageLogger : IHttpMessageLogger
  {
    private readonly StringBuilder logger = new StringBuilder();

    public string GetLog(bool clear)
    {
      var log = logger.ToString();
      if (clear)
      {
        logger.Clear();
      }
      return log;
    }

    public Task WriteLine(string value)
    {
      logger.AppendLine(value);
      return Task.CompletedTask;
    }
  }
}
