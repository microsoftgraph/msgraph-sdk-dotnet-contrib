using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SPChangeQueryConverter : JsonConverter<ChangeQuery>
  {
    public override ChangeQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return null;
    }

    public override void Write(Utf8JsonWriter writer, ChangeQuery value, JsonSerializerOptions options)
    {
      writer.WriteStartObject();

      foreach (var property in value.GetType().GetProperties())
      {
        if (property.PropertyType == typeof(bool))
        {
          ConditionallyWriteBooleanProperty(writer, property, value);
        }
        else
        {
          if (property.Name == nameof(ChangeQuery.ChangeTokenEnd))
          {
              if (property.GetValue(value) != null)
              {
                JsonSerializer.Serialize(writer, property.GetValue(value));
              }
          }
        }
      }

      writer.WriteEndObject();
    }

    private void ConditionallyWriteBooleanProperty(Utf8JsonWriter writer, PropertyInfo property, ChangeQuery value)
    {
      if ((bool)property.GetValue(value))
      {
        writer.WriteBoolean(property.Name, true);
      }
    }
  }
}
