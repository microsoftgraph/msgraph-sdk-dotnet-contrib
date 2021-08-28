using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SPEnumIntAsStringConverter<T> : JsonConverter<T>
      where T : struct, Enum
  {
    public override bool CanConvert(Type type)
    {
      return type.IsEnum;
    }

    public SPEnumIntAsStringConverter() { }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      JsonTokenType token = reader.TokenType;

      if (token == JsonTokenType.String)
      {
        string enumString = reader.GetString();

        if (Int32.TryParse(enumString, out var value))
        {
          return Unsafe.As<int, T>(ref value);
        }
      }

      return default;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
      var valAsInt = Unsafe.As<T, int>(ref value);

      writer.WriteStringValue(valAsInt.ToString());
    }
  }
}
