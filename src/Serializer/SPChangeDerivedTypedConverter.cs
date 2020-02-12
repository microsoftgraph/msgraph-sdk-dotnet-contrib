// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

namespace Graph.Community
{
  using System;
  using System.Collections.Concurrent;
  using System.Linq;
  using System.Reflection;
  using Microsoft.Graph;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

#pragma warning disable CA1307 // Specify StringComparison

  /// <summary>
  /// Handles resolving interfaces to the correct derived class during serialization/deserialization.
  /// </summary>
  public class SPChangeDerivedTypedConverter : JsonConverter
  {
    internal static readonly ConcurrentDictionary<string, Type> TypeMappingCache = new ConcurrentDictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Constructs a new DerivedTypeConverter.
    /// </summary>
    public SPChangeDerivedTypedConverter()
        : base()
    {
    }

    /// <summary>
    /// Checks if the given object can be converted. In this instance, all object can be converted.
    /// </summary>
    /// <param name="objectType">The type of the object to convert.</param>
    /// <returns>True</returns>
    public override bool CanConvert(Type objectType)
    {
      return true;
    }

    /// <summary>
    /// Checks if the entity supports write. Currently no derived types support write.
    /// </summary>
    public override bool CanWrite
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Deserializes the object to the correct type.
    /// </summary>
    /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
    /// <param name="objectType">The interface type.</param>
    /// <param name="existingValue">The existing value of the object being read.</param>
    /// <param name="serializer">The <see cref="JsonSerializer"/> for deserialization.</param>
    /// <returns></returns>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (reader == null)
      {
        throw new ArgumentNullException(nameof(reader));
      }
      if (objectType == null)
      {
        throw new ArgumentNullException(nameof(objectType));
      }
      if (serializer == null)
      {
        throw new ArgumentNullException(nameof(serializer));
      }

      var jObject = JObject.Load(reader);

      var type = jObject.GetValue(CoreConstants.Serialization.ODataType);

      object instance;
      if (type != null)
      {
        var typeString = type.ToString();
        typeString = typeString.Replace("#SP", "Graph.Community");
        typeString = StringHelper.ConvertTypeToTitleCase(typeString);

        Type instanceType = null;

        if (TypeMappingCache.TryGetValue(typeString, out instanceType))
        {
          instance = this.Create(instanceType);
        }
        else
        {
          var typeAssembly = objectType.GetTypeInfo().Assembly;
          instance = this.Create(typeString, typeAssembly);
        }

        // If @odata.type is set but we aren't able to create an instance of it use the method-provided
        // object type instead. This means unknown types will be deserialized as a parent type.
        if (instance == null)
        {
          instance = this.Create(objectType.AssemblyQualifiedName, /* typeAssembly */ null);
        }

        if (instance != null && instanceType == null)
        {
          // Cache the type mapping resolution if we haven't pulled it from the cache already.
          TypeMappingCache.TryAdd(typeString, instance.GetType());
        }
      }
      else
      {
        instance = this.Create(objectType.AssemblyQualifiedName, /* typeAssembly */ null);
      }

      if (instance == null)
      {
        throw new ServiceException(
            new Error
            {
              Code = "generalException",
              Message = $"Unable to create an instance of type {objectType.AssemblyQualifiedName}.",
            });
      }

      using (var objectReader = this.GetObjectReader(reader, jObject))
      {
        serializer.Populate(objectReader, instance);
        return instance;
      }
    }

    /// <summary>
    /// Not yet implemented
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
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

#pragma warning disable CA1822
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

        return constructorInfo.Invoke(Array.Empty<object>());
      }
      catch (Exception exception)
      {
        throw new ServiceException(
            new Error
            {
              Code = "generalException",
              Message = $"Unable to create an instance of type {type.FullName}."
            },
            exception);
      }
    }
#pragma warning restore CA1822

    private JsonReader GetObjectReader(JsonReader originalReader, JObject jObject)
    {
      var objectReader = jObject.CreateReader();

      objectReader.Culture = originalReader.Culture;
      objectReader.DateParseHandling = originalReader.DateParseHandling;
      objectReader.DateTimeZoneHandling = originalReader.DateTimeZoneHandling;
      objectReader.FloatParseHandling = originalReader.FloatParseHandling;

      // This reader is only for a single token. Don't dispose the stream after.
      objectReader.CloseInput = false;

      return objectReader;
    }
  }
#pragma warning restore CA1307 // Specify StringComparison
}
