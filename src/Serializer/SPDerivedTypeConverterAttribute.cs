using System;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  [AttributeUsage(AttributeTargets.Class)]
  public class SPDerivedTypeConverterAttribute : JsonConverterAttribute
  {
    /// <summary>
    /// Initializes a new instance of <see cref="JsonConverterAttribute"/> with the specified converter type.
    /// </summary>
    /// <param name="converterType">The type of the converter.</param>
    public SPDerivedTypeConverterAttribute(Type converterType)
    : base(converterType)
    {
    }

  }
}
