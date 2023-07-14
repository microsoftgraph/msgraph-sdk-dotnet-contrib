using Microsoft.Graph;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graph.Community.Test
{
  /// <summary>
  /// An <see cref="ISerializer"/> implementation using the JSON.NET serializer.
  /// </summary>
  public class TestSerializer : Microsoft.Graph.ISerializer
  {
    readonly JsonSerializerOptions jsonSerializerOptions;

    public TestSerializer()
    : this(
          new JsonSerializerOptions
          {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
          })
    {
    }

    public TestSerializer(JsonSerializerOptions jsonSerializerSettings)
    {
      this.jsonSerializerOptions = jsonSerializerSettings;
      this.jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      this.jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
      this.jsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
    }

    public T DeserializeObject<T>(Stream stream)
    {
      // Wrapping the provided class because stream.Length is not implement on EmptyStream
      //  (Part of the .Net 3 -> .Net 6 update)
      //if (stream == null || stream.Length == 0)
      if (stream == null)
      {
        return default;
      }

      try
      {
        return JsonSerializer.DeserializeAsync<T>(stream, this.jsonSerializerOptions).GetAwaiter().GetResult();
      }
      catch (JsonException)
      {
        return default;
      }
    }

    public T DeserializeObject<T>(string inputString)
    {
      if (string.IsNullOrEmpty(inputString))
      {
        return default(T);
      }

      return JsonSerializer.Deserialize<T>(inputString, this.jsonSerializerOptions);
    }

    public string SerializeObject(object serializeableObject)
    {
      if (serializeableObject == null)
      {
        return null;
      }

      var stream = serializeableObject as Stream;
      if (stream != null)
      {
        using (var streamReader = new StreamReader(stream))
        {
          return streamReader.ReadToEnd();
        }
      }

      var stringValue = serializeableObject as string;
      if (stringValue != null)
      {
        return stringValue;
      }

      return JsonSerializer.Serialize(serializeableObject, this.jsonSerializerOptions);
    }
  }
}

