using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Graph.Community
{
  public class TenantSettingsResponse
  {
    [JsonPropertyName("CorporateCatalogUrl")]
    public string CorporateCatalogUrl { get; set; }
  }
}
