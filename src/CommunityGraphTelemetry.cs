using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace Graph.Community
{
  internal class CommunityGraphTelemetry
  {
    private static readonly TelemetryConfiguration telemetryConfiguration = TelemetryConfiguration.CreateDefault();
    private static readonly TelemetryClient telemetryClient;

    static CommunityGraphTelemetry()
    {
      telemetryConfiguration.ConnectionString= "InstrumentationKey=d882bd7a-a378-4117-bd7c-71fc95a44cd1;IngestionEndpoint=https://centralus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://centralus.livediagnostics.monitor.azure.com/";
      telemetryClient = new TelemetryClient(telemetryConfiguration);
    }

    internal static void LogFactoryMethod(LoggingOptions loggingOptions)
    {
      if (CommunityGraphClientFactory.TelemetryDisabled)
      {
        return;
      }

      Dictionary<string, string> properties = new Dictionary<string, string>(4)
      {
        { CommunityGraphConstants.Headers.CommunityLibraryVersionHeaderName, CommunityGraphConstants.Library.AssemblyVersion },
        { CommunityGraphConstants.TelemetryProperties.AuthenticationProvider, loggingOptions.AuthenticationProvider },
        { CommunityGraphConstants.TelemetryProperties.TokenCredential, loggingOptions.TokenCredential },
        { CommunityGraphConstants.TelemetryProperties.LoggingHandler, loggingOptions.LoggingHandler.ToString() }
      };

      telemetryClient.TrackEvent("CommunityGraphClientFactory", properties);
      telemetryClient.Flush();
    }

    internal static void LogServiceRequest(
      string resourceUri,
      string clientRequestId,
      System.Net.Http.HttpMethod requestMethod,
      System.Net.HttpStatusCode statusCode,
      string rawResponseContent)
    {
      if (CommunityGraphClientFactory.TelemetryDisabled)
      {
        return;
      }

      Dictionary<string, string> properties = new Dictionary<string, string>(5)
      {
        { CommunityGraphConstants.Headers.CommunityLibraryVersionHeaderName, CommunityGraphConstants.Library.AssemblyVersion },
        { CommunityGraphConstants.TelemetryProperties.ResourceUri, resourceUri },
        { CommunityGraphConstants.TelemetryProperties.RequestMethod, requestMethod.ToString() },
        { CommunityGraphConstants.TelemetryProperties.ClientRequestId, clientRequestId },
        { CommunityGraphConstants.TelemetryProperties.ResponseStatus, $"{statusCode} ({(int)statusCode})" }
      };

      if (!string.IsNullOrEmpty(rawResponseContent))
      {
        properties.Add(CommunityGraphConstants.TelemetryProperties.RawErrorResponse, rawResponseContent);
      }

      telemetryClient.TrackEvent("GraphCommunityRequest", properties);
      telemetryClient.Flush();
    }

    internal static void LogExtensionMethod(string extensionMethodName = "Not specified")
    {
      if (CommunityGraphClientFactory.TelemetryDisabled)
      {
        return;
      }

      Dictionary<string, string> properties = new Dictionary<string, string>(2)
      {
        { CommunityGraphConstants.Headers.CommunityLibraryVersionHeaderName, CommunityGraphConstants.Library.AssemblyVersion },
        { CommunityGraphConstants.TelemetryProperties.ExtensionMethod, extensionMethodName },
      };

      telemetryClient.TrackEvent("GraphCommunityExtensionMethod", properties);
      telemetryClient.Flush();
    }
  }
}
