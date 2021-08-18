using System;
using System.Reflection;
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
          if (property.Name == nameof(ChangeQuery.ChangeTokenEnd) ||
              property.Name == nameof(ChangeQuery.ChangeTokenStart))
          {
            if (property.GetValue(value) != null)
            {
              var token = property.GetValue(value) as ChangeToken;
              writer.WriteStartObject(property.Name);
              writer.WriteString(nameof(token.StringValue), token.StringValue);
              writer.WriteEndObject();
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
