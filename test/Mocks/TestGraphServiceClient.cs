using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using System;
using System.Net.Http;

namespace Graph.Community.Test
{
  public class TestGraphServiceClient : IDisposable
  {
    public GraphServiceClient GraphServiceClient { get; set; }
    public MockHttpProvider HttpProvider { get; set; }

    private readonly HttpResponseMessage httpResponseMessage;
    private readonly bool disposeHttpResponseMessage = false;

    public TestGraphServiceClient(HttpResponseMessage httpResponseMessage = null)
    {
      if (httpResponseMessage == null)
      {
        this.httpResponseMessage = new HttpResponseMessage();
        disposeHttpResponseMessage = true;
      }
      else
      {
        this.httpResponseMessage = httpResponseMessage;
      }

      var ap = new MockAuthenticationProvider();
      var ser = new Graph.Community.Test.TestSerializer();
      this.HttpProvider = new MockHttpProvider(this.httpResponseMessage, ser);
      this.GraphServiceClient = new GraphServiceClient(ap.Object, this.HttpProvider.Object);
    }

    public TestGraphServiceClient(HttpMessageHandler handler, HttpResponseMessage httpResponseMessage = null)
    {
      if (httpResponseMessage == null)
      {
        this.httpResponseMessage = new HttpResponseMessage();
        disposeHttpResponseMessage = true;
      }
      else
      {
        this.httpResponseMessage = httpResponseMessage;
      }

      var ap = new MockAuthenticationProvider();
      var ser = new Graph.Community.Test.TestSerializer();
      this.HttpProvider = new MockHttpProvider(handler, this.httpResponseMessage);
      this.GraphServiceClient = new GraphServiceClient(ap.Object, this.HttpProvider.Object);

    }


    public static TestGraphServiceClient Create(HttpResponseMessage httpResponseMessage = null)
    {
      CommunityGraphClientFactory.telemetryDisabled = true;
      return new TestGraphServiceClient(httpResponseMessage);
    }

    public static TestGraphServiceClient Create(HttpMessageHandler handler, HttpResponseMessage httpResponseMessage = null)
    {
      CommunityGraphClientFactory.telemetryDisabled = true;
      return new TestGraphServiceClient(handler, httpResponseMessage);
    }


    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          if (disposeHttpResponseMessage)
          {
            httpResponseMessage.Dispose();
          }
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }


    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~GraphServiceTestClient()
    // {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion

  }
}
