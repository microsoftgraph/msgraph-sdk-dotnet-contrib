// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Net;
using System.Text;

namespace Graph.Community.Diagnostics
{
  [EventSource(Name = EventSourceName)]
  internal sealed class GraphCommunityEventSource : EventSource
  {

    /*
     *   This is pretty generic stuff, copied from Azure.Core. Intended to support all the Azure SDKs...
     * 
     * 
     *   I only need to log events from the middleware. So perhaps a custom eventArgs class to store data. 
     *   - handler name
     *   - handler options
     *   - request id
     *   - event-specific payload...
     */




    private const string EventSourceName = "Graph-Community";

    private const int SharePointServiceHandlerPreprocessEvent = 1;
    private const int SharePointServiceHandlerPostprocessEvent = 2;
    private const int SharePointServiceHandlerNonsuccessEvent = 3;
 
    private GraphCommunityEventSource()
      : base(EventSourceName, EventSourceSettings.Default, GraphCommunityEventSourceListener.TraitName, GraphCommunityEventSourceListener.TraitValue)
    {
    }

    public static GraphCommunityEventSource Singleton { get; } = new GraphCommunityEventSource();


    [NonEvent]
    public void SharePointServiceHandlerPreprocess(string resourceUri, GraphRequestContext context)
    {
      if (IsEnabled(EventLevel.Informational, EventKeywords.All))
      {
        SharePointServiceHandlerPreprocess(resourceUri, context.ClientRequestId);
      }
    }

    [Event(SharePointServiceHandlerPreprocessEvent, Level = EventLevel.Informational, Message = "ResourceUri: {0} ClientRequestId: {1}")]
    public void SharePointServiceHandlerPreprocess(string resourceUri, string clientRequestId)
    {
      WriteEvent(SharePointServiceHandlerPreprocessEvent, resourceUri, clientRequestId);
    }

    [NonEvent]
    public void SharePointServiceHandlerPostprocess(string resourceUri, GraphRequestContext context, HttpStatusCode statusCode)
    {
      if (IsEnabled(EventLevel.Informational, EventKeywords.All))
      {
        SharePointServiceHandlerPostprocess(resourceUri, context.ClientRequestId, $"{statusCode} ({(int)statusCode}");
      }
    }

    [Event(SharePointServiceHandlerPostprocessEvent, Level = EventLevel.Informational, Message = "ResourceUri: {0} ClientRequestId: {1} StatusCode: {2}")]
    public void SharePointServiceHandlerPostprocess(string resourceUri, string clientRequestId, string statusCode)
    {
      WriteEvent(SharePointServiceHandlerPostprocessEvent, resourceUri, clientRequestId, statusCode);
    }

    [NonEvent]
    public void SharePointServiceHandlerNonsuccess(string resourceUri, GraphRequestContext context, string rawResponseContent)
    {
      if (IsEnabled(EventLevel.Informational, EventKeywords.All))
      {
        SharePointServiceHandlerNonsuccess(resourceUri, context.ClientRequestId, rawResponseContent);
      }
    }

    [Event(SharePointServiceHandlerNonsuccessEvent, Level = EventLevel.Informational, Message = "{0} returned non-success. RawResponse: {1} ClientRequestId: {2}")]
    public void SharePointServiceHandlerNonsuccess(string resourceUri, string clientRequestId, string responseBody)
    {
      WriteEvent(SharePointServiceHandlerNonsuccessEvent, resourceUri, clientRequestId, responseBody);
    }

    [NonEvent]
    private static string FormatException(Exception ex)
    {
      StringBuilder sb = new StringBuilder();
      bool nest = false;
      do
      {
        if (nest)
        {
          // Format how Exception.ToString() would.
          sb.AppendLine()
            .Append(" ---> ");
        }
        // Do not include StackTrace, but do include HResult (often useful for CryptographicExceptions or IOExceptions).
        sb.Append(ex.GetType().FullName)
          .Append(" (0x")
          .Append(ex.HResult.ToString("x", CultureInfo.InvariantCulture))
          .Append("): ")
          .Append(ex.Message);
        ex = ex.InnerException;
        nest = true;
      }
      while (ex != null);
      return sb.ToString();
    }

    [NonEvent]
    private static string FormatStringArray(string[] array)
    {
      return new StringBuilder("[ ").Append(string.Join(", ", array)).Append(" ]").ToString();
    }
  }
}
