// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable enable

namespace Graph.Community.Diagnostics
{
  /// <summary>
  /// Implementation of <see cref="EventListener"/> that listens to events produces by Azure SDK Client libraries.
  /// </summary>
  public class GraphCommunityEventSourceListener : EventListener
  {
    /// <summary>
    /// The trait name that has to be present on all event sources collected by this listener.
    /// </summary>
    public const string TraitName = "GraphCommunityEventSource";
    /// <summary>
    /// The trait value that has to be present on all event sources collected by this listener.
    /// </summary>
    public const string TraitValue = "true";

    private readonly List<EventSource> _eventSources = new List<EventSource>();

    private readonly Action<EventWrittenEventArgs, string> _log;
    private readonly EventLevel _level;

    /// <summary>
    /// Creates an instance of <see cref="GraphCommunityEventSourceListener"/> that executes a <paramref name="log"/> callback every time event is written.
    /// </summary>
    /// <param name="log">The <see cref="System.Action{EventWrittenEventArgs, String}"/> to call when event is written. The second parameter is formatted message.</param>
    /// <param name="level">The level of events to enable.</param>
    public GraphCommunityEventSourceListener(Action<EventWrittenEventArgs, string> log, EventLevel level)
    {
      _log = log ?? throw new ArgumentNullException(nameof(log));

      _level = level;

      foreach (EventSource eventSource in _eventSources)
      {
        OnEventSourceCreated(eventSource);
      }

      _eventSources.Clear();
    }

    /// <inheritdoc />
    protected sealed override void OnEventSourceCreated(EventSource eventSource)
    {
      base.OnEventSourceCreated(eventSource);

      if (_log == null)
      {
        _eventSources.Add(eventSource);
      }

      if (eventSource.GetTrait(TraitName) == TraitValue)
      {
        EnableEvents(eventSource, _level);
      }
    }

    /// <inheritdoc />
    protected sealed override void OnEventWritten(EventWrittenEventArgs eventData)
    {
      // Workaround https://github.com/dotnet/corefx/issues/42600
      if (eventData.EventId == -1)
      {
        return;
      }

      // There is a very tight race during the AzureEventSourceListener creation where EnableEvents was called
      // and the thread producing events not observing the `_log` field assignment
      _log?.Invoke(eventData, EventSourceEventFormatting.Format(eventData));
    }

    /// <summary>
    /// Creates a new instance of <see cref="GraphCommunityEventSourceListener"/> that forwards events to <see cref="Console.WriteLine(string)"/>.
    /// </summary>
    /// <param name="level">The level of events to enable.</param>
    public static GraphCommunityEventSourceListener CreateConsoleLogger(EventLevel level = EventLevel.Informational)
    {
      return new GraphCommunityEventSourceListener((eventData, text) => Console.WriteLine("[{1}] {0}: {2}", eventData.EventSource.Name, eventData.Level, text), level);
    }

    /// <summary>
    /// Creates a new instance of <see cref="GraphCommunityEventSourceListener"/> that forwards events to <see cref="Trace.WriteLine(object)"/>.
    /// </summary>
    /// <param name="level">The level of events to enable.</param>
    public static GraphCommunityEventSourceListener CreateTraceLogger(EventLevel level = EventLevel.Informational)
    {
      return new GraphCommunityEventSourceListener(
          (eventData, text) => Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "[{0}] {1}", eventData.Level, text), eventData.EventSource.Name), level);
    }
  }




  internal static class EventSourceEventFormatting
  {
    public static string Format(EventWrittenEventArgs eventData)
    {
      var payloadArray = eventData.Payload?.ToArray() ?? Array.Empty<object?>();

      ProcessPayloadArray(payloadArray);

      if (eventData.Message != null)
      {
        try
        {
          return string.Format(CultureInfo.InvariantCulture, eventData.Message, payloadArray);
        }
        catch (FormatException)
        {
        }
      }

      var stringBuilder = new StringBuilder();
      stringBuilder.Append(eventData.EventName);

      if (!string.IsNullOrWhiteSpace(eventData.Message))
      {
        stringBuilder.AppendLine();
        stringBuilder.Append(nameof(eventData.Message)).Append(" = ").Append(eventData.Message);
      }

      if (eventData.PayloadNames != null)
      {
        for (int i = 0; i < eventData.PayloadNames.Count; i++)
        {
          stringBuilder.AppendLine();
          stringBuilder.Append(eventData.PayloadNames[i]).Append(" = ").Append(payloadArray[i]);
        }
      }

      return stringBuilder.ToString();
    }

    private static void ProcessPayloadArray(object?[] payloadArray)
    {
      for (int i = 0; i < payloadArray.Length; i++)
      {
        payloadArray[i] = FormatValue(payloadArray[i]);
      }
    }

    private static object? FormatValue(object? o)
    {
      if (o is byte[] bytes)
      {
        var stringBuilder = new StringBuilder();
        foreach (byte b in bytes)
        {
          stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
        }

        return stringBuilder.ToString();
      }

      return o;
    }
  }
}


