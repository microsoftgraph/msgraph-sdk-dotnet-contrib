using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Graph.Community
{
  internal class SitePagePromotedStateConverterFactory : JsonConverterFactory
  {
    public override bool CanConvert(Type typeToConvert)
    {
      return typeToConvert.IsAssignableFrom(typeof(SitePagePromotedState));
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
      return new SitePagePromotedStateEnumConverter();
    }
  }

  internal class SitePagePromotedStateEnumConverter : JsonConverter<SitePagePromotedState>
  {
    public override SitePagePromotedState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {

      SitePagePromotedState result = SitePagePromotedState.NotPromoted;

      if (reader.TokenType == JsonTokenType.Number)
      {
        // Get the value.
        Double value;
        value = JsonSerializer.Deserialize<Double>(ref reader, options)!;

        int valueAsInt = Convert.ToInt32(value);
        result = (SitePagePromotedState)valueAsInt;
      }
      else if (reader.TokenType == JsonTokenType.String)
      {
        string value;
        value = JsonSerializer.Deserialize<string>(ref reader, options)!;
        if (Enum.TryParse(typeToConvert, value, out var r))
        {
          result = (SitePagePromotedState)r;
        }
        else
        {
          throw new JsonException();
        }
      }
      else
      {
        throw new JsonException();
      }

      return result;
    }

    public override void Write(Utf8JsonWriter writer, SitePagePromotedState value, JsonSerializerOptions options)
    {
      JsonSerializer.Serialize(writer, value, options);
    }
  }
}
