using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Graph;
using Microsoft.Graph.SecurityNamespace;

namespace Graph.Community
{
  [DebuggerDisplay("ODataType: {ODataType, nq}")]
  [SPDerivedTypeConverter(typeof(SPODataTypeConverter<Field>))]
  public class Field : BaseItem
  {
    /// <summary>
    /// Gets or sets @odata.type.
    /// </summary>
    [JsonPropertyName("odata.type")]
    public new string ODataType { get; set; }

    [JsonPropertyName("ID")]
    public new Guid Id { get; set; }


    [JsonPropertyName("ClientValidationFormula")]
    public string ClientValidationFormula { get; set; }
    [JsonPropertyName("ClientValidationMessage")]
    public string ClientValidationMessage { get; set; }
    [JsonPropertyName("CustomFormatter")]
    public string CustomFormatter { get; set; }
    [JsonPropertyName("DefaultFormula")]
    public string DefaultFormula { get; set; }
    [JsonPropertyName("DefaultValue")]
    public string DefaultValue { get; set; }
    //public string Description { get; set; }
    [JsonPropertyName("EnforceUniqueValues")]
    public bool EnforceUniqueValues { get; set; }
    [JsonPropertyName("Group")]
    public string Group { get; set; }
    [JsonPropertyName("Hidden")]
    public bool Hidden { get; set; }
    [JsonPropertyName("InternalName")]
    public string InternalName { get; set; }
    [JsonPropertyName("JSLink")]
    public string JSLink { get; set; }
    [JsonPropertyName("ReadOnlyField")]
    public bool ReadOnlyField { get; set; }
    [JsonPropertyName("Required")]
    public bool Required { get; set; }
    [JsonPropertyName("SchemaXml")]
    public string SchemaXml { get; set; }
    [JsonPropertyName("Scope")]
    public string Scope { get; set; }
    [JsonPropertyName("Sealed")]
    public bool Sealed { get; set; }
    [JsonPropertyName("Title")]
    public string Title { get; set; }
    [JsonPropertyName("TypeAsString")]
    public string TypeAsString { get; set; }
    [JsonPropertyName("TypeDisplayName")]
    public string TypeDisplayName { get; set; }
    [JsonPropertyName("TypeShortDescription")]
    public string TypeShortDescription { get; set; }
    [JsonPropertyName("ValidationFormula")]
    public string ValidationFormula { get; set; }

    [JsonPropertyName("ValidationMessage")]
    public string ValidationMessage { get; set; }
  }
}
