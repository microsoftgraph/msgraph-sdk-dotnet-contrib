using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community
{
  public class LoggingMessageHandler : DelegatingHandler
  {
    private readonly IHttpMessageLogger logger;

    public LoggingMessageHandler()
      : this(null)
    {
    }

    public LoggingMessageHandler(IHttpMessageLogger logger, HttpMessageHandler innerHandler = null)
    {
      InnerHandler = innerHandler ?? new HttpClientHandler();
      this.logger = logger ?? new NullHttpMessageLogger();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      if (request.Content != null)
      {
        await request.Content.LoadIntoBufferAsync();
      }

      using (var messageFormatter = new HttpMessageFormatter(request))
      {
        await logger.WriteLine(await messageFormatter.ReadAsStringAsync());
        await logger.WriteLine("");
      }

      var stopWatch = new Stopwatch();
      stopWatch.Start();

      var response = await base.SendAsync(request, cancellationToken);

      stopWatch.Stop();


      if (response.Content != null)
      {
        await response.Content.LoadIntoBufferAsync();
      }
      using (var messageFormatter = new HttpMessageFormatter(response))
      {
        await logger.WriteLine(await messageFormatter.ReadAsStringAsync());
        await logger.WriteLine("");
        await logger.WriteLine("Roundtrip (ms): " + stopWatch.ElapsedMilliseconds);
        await logger.WriteLine("================================================");
      }

      return response;
    }

  }
}
