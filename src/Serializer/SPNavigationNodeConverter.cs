using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class SPNavigationNodeConverter : JsonConverter<NavigationNode>
  {
    public override NavigationNode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var instance = this.Create(typeToConvert.AssemblyQualifiedName, /* typeAssembly */ null);

      if (instance == null)
      {
        throw new Microsoft.Graph.ServiceException(
            new Microsoft.Graph.Error
            {
              Code = "generalException", //ErrorConstants.Codes.GeneralException,
              Message = string.Format(
                    "Unable to create an instance of type {0}.", //ErrorConstants.Messages.UnableToCreateInstanceOfTypeFormatString,
                    typeToConvert.AssemblyQualifiedName),
            });
      }

      JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

      PopulateObject(instance, jsonDocument.RootElement, options);
      return (NavigationNode)instance;
    }

    private void PopulateObject(object target, JsonElement json, JsonSerializerOptions options)
    {
      // We use the target type information since it maybe be derived. We do not want to leave out extra properties in the child class and put them in the additional data unnecessarily
      Type objectType = target.GetType();
      switch (json.ValueKind)
      {
        case JsonValueKind.Object:
          {
            // iterate through the object properties
            foreach (var property in json.EnumerateObject())
            {
              // look up the property in the object definition using the mapping provided in the model attribute
              var propertyInfo = objectType.GetProperties().FirstOrDefault((mappedProperty) =>
              {
                var attribute = mappedProperty.GetCustomAttribute<JsonPropertyNameAttribute>();
                return attribute?.Name == property.Name;
              });
              if (propertyInfo == null)
              {
                //Add the property to AdditionalData as it doesn't exist as a member of the object
                AddToAdditionalDataBag(target, objectType, property);
                continue;
              }

              try
              {
                // Deserialize the property in and update the current object.
                var parsedValue = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
                propertyInfo.SetValue(target, parsedValue);
              }
              catch (JsonException)
              {
                //Add the property to AdditionalData as it can't be deserialized as a member. Eg. non existing enum member type
                AddToAdditionalDataBag(target, objectType, property);
              }
            }

            break;
          }
        case JsonValueKind.Array:
          {
            //Its most likely a collectionPage instance so get its CurrentPage property
            var collectionPropertyInfo = objectType.GetProperty("CurrentPage", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            if (collectionPropertyInfo != null)
            {
              // Get the generic type info for deserialization
              Type genericType = collectionPropertyInfo.PropertyType.GenericTypeArguments.FirstOrDefault();
              int index = 0;
              foreach (var property in json.EnumerateArray())
              {
                // Get the object instance
                var instance = JsonSerializer.Deserialize(property.GetRawText(), genericType, options);

                // Invoke the insert function to add it to the collection as it an IList
                MethodInfo methodInfo = collectionPropertyInfo.PropertyType.GetMethods().FirstOrDefault(method => method.Name.Equals("Insert"));
                object[] parameters = new object[] { index, instance };
                if (methodInfo != null)
                {
                  methodInfo.Invoke(target, parameters);//insert the object to the page List
                  index++;
                }
              }
            }

            break;
          }
      }
    }

    private void AddToAdditionalDataBag(object target, Type objectType, JsonProperty property)
    {
      // Get the property with the JsonExtensionData attribute and add the property to the collection
      var additionalDataInfo = objectType.GetProperties().FirstOrDefault(propertyInfo => ((MemberInfo)propertyInfo).GetCustomAttribute<JsonExtensionDataAttribute>() != null);
      if (additionalDataInfo != null)
      {
        var additionalData = additionalDataInfo.GetValue(target) as IDictionary<string, object> ?? new Dictionary<string, object>();
        additionalData.Add(property.Name, property.Value);
        additionalDataInfo.SetValue(target, additionalData);
      }
    }

    public override void Write(Utf8JsonWriter writer, NavigationNode value, JsonSerializerOptions options)
    {
      writer.WriteStartObject();

      if (value.Id != default)
      {
        writer.WriteNumber(nameof(value.Id), value.Id);
      }
      if (value.Title != default)
      {
        writer.WriteString(nameof(value.Title), value.Title);
      }
      if (value.Url != default)
      {
        writer.WriteString(nameof(value.Url), value.Url.ToString().TrimEnd('/'));
      }
      if (value.IsDocLib != default)
      {
        writer.WriteBoolean(nameof(value.IsDocLib), value.IsDocLib);
      }
      if (value.IsExternal != default)
      {
        writer.WriteBoolean(nameof(value.IsExternal), value.IsExternal);
      }
      if (value.IsVisible != default)
      {
        writer.WriteBoolean(nameof(value.IsVisible), value.IsVisible);
      }
      if (value.ListTemplateType != default)
      {
        writer.WriteNumber(nameof(value.ListTemplateType), value.ListTemplateType);
      }

      writer.WriteEndObject();
    }

    private object Create(string typeString, Assembly typeAssembly)
    {
      Type type = null;

      if (typeAssembly != null)
      {
        type = typeAssembly.GetType(typeString);
      }
      else
      {
        type = Type.GetType(typeString);
      }

      return this.Create(type);
    }

    private object Create(Type type)
    {
      if (type == null)
      {
        return null;
      }

      try
      {
        // Find the default constructor. Abstract entity classes use non-public constructors.
        var constructorInfo = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault(
            constructor => !constructor.GetParameters().Any() && !constructor.IsStatic);

        if (constructorInfo == null)
        {
          return null;
        }

        return constructorInfo.Invoke(new object[] { }); ;
      }
      catch (Exception exception)
      {
        throw new Microsoft.Graph.ServiceException(
            new Microsoft.Graph.Error
            {
              Code = "generalException", //ErrorConstants.Codes.GeneralException,
              Message = string.Format("Unable to create an instance of type {0}.", type.FullName),
            },
            exception);
      }
    }

  }
}
