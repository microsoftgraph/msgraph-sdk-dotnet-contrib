using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public class BaseSharePointAPIRequest : BaseRequest
  {
    private readonly string resourceUri;

    public BaseSharePointAPIRequest(
      string resourceUri,
      string requestUrl,
      IBaseClient client,
      IEnumerable<Option> options)
      : base(requestUrl, client, options)
    {
      this.resourceUri = resourceUri;
    }

    public bool TelemetryDisabled
    {
      get
      {
        return CommunityGraphClientFactory.TelemetryDisabled;
      }
    }


    //
    // Summary:
    //     Sends the request.
    //
    // Parameters:
    //   serializableObject:
    //     The serializable object to send.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken for the request.
    //
    //   completionOption:
    //     The System.Net.Http.HttpCompletionOption to pass to the Microsoft.Graph.IHttpProvider
    //     on send.
    //
    // Type parameters:
    //   T:
    //     The expected response object type for deserialization.
    //
    // Returns:
    //     The deserialized response object.
    public new Task<T> SendAsync<T>(object serializableObject, CancellationToken cancellationToken, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
    {
      SetHandlerOptions();
      return base.SendAsync<T>(serializableObject, cancellationToken, completionOption);
    }

    //
    // Summary:
    //     Sends the request.
    //
    // Parameters:
    //   serializableObject:
    //     The serializable object to send.
    //
    //   cancellationToken:
    //     The System.Threading.CancellationToken for the request.
    //
    //   completionOption:
    //     The System.Net.Http.HttpCompletionOption to pass to the Microsoft.Graph.IHttpProvider
    //     on send.
    //
    // Returns:
    //     The task to await.
    public new Task SendAsync(object serializableObject, CancellationToken cancellationToken, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
    {
      SetHandlerOptions();
      return base.SendAsync(serializableObject, cancellationToken, completionOption);
    }

    private void SetHandlerOptions()
    {
      SetHandlerOptions(CommunityGraphClientFactory.TelemetryDisabled);
    }

    private void SetHandlerOptions(bool telemetryDisabled)
    {
      SharePointServiceHandlerOption handlerOptions = default;

      string handlerOptionKey = typeof(SharePointServiceHandlerOption).Name;

      if (!this.MiddlewareOptions.ContainsKey(handlerOptionKey))
      {
        handlerOptions = new SharePointServiceHandlerOption()
        {
          DisableTelemetry = telemetryDisabled,
          ResourceUri = this.resourceUri
        };
        this.MiddlewareOptions[handlerOptionKey] = handlerOptions;
      }
      else
      {
        handlerOptions = this.MiddlewareOptions[handlerOptionKey] as SharePointServiceHandlerOption;
        handlerOptions.ResourceUri = this.resourceUri;
      }
    }

    public BaseRequest WithTelemetryDisabled()
    {
      SetHandlerOptions(true);
      return this;
    }
  }
}
