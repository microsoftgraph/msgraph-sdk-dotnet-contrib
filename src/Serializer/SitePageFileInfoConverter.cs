using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SitePageFileInfoConverter : JsonConverter<SitePageFileInfo>
  {
    public override SitePageFileInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, SitePageFileInfo value, JsonSerializerOptions options)
    {
      writer.WriteStartObject();
      writer.WritePropertyName("odata.type");
      writer.WriteStringValue("Graph.Community.SitePageFileInfo");
      //JsonSerializer.Serialize(writer, value, options);

      value.GetType().GetProperties()
              .Where(x => !Attribute.IsDefined(x, typeof(JsonIgnoreAttribute)))
              .ToList().ForEach(property =>
              {
                var propertyJsonName = ConvertPropertyName(options, property.Name);
                var propertyValue = value.GetType().GetProperty(property.Name)?.GetValue(value);

                if (propertyValue != null || !options.IgnoreNullValues)
                {
                  writer.WritePropertyName(propertyJsonName);
                  JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
                }
              });


      writer.WriteEndObject();
    }

    string ConvertPropertyName(JsonSerializerOptions options, string name)
    {
      if (options.PropertyNamingPolicy != null)
        return options.PropertyNamingPolicy.ConvertName(name);

      return name;
    }
  }
}
